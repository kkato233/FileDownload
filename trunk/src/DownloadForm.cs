// 2008.02.24 例外が発生した場合の処理をちゃんと記述
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace FileDownloadApplication
{
    /// <summary>
    /// ダウンロードのためのフォーム
    /// </summary>
    /// <remarks>
    /// ユーザからURLを入力してもらいそのURLをダウンロードします。
    /// ダウンロードは別スレッドで行うため、ダウンロードの進捗状況を表示する処理等は別スレッドからフォームの表示を更新できるように注意して実装します。
    /// </remarks>
    public partial class DownloadForm : Form
    {
        /// <summary>
        /// ダウンロードフォームのコンストラクタです。
        /// </summary>
        public DownloadForm()
        {
            InitializeComponent();

            SetDefaultDownloadPath();

            OpenURLList(this.textDownloadPath.Text);

            System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

            this.Text += " ver " + ver.Major.ToString() + "." + ver.Minor.ToString() + "." + ver.Build.ToString();
 
        }

        /// <summary>
        /// デフォルトのダウンロードディレクトリを設定する。
        /// </summary>
        /// <remarks>
        /// ダウンロードのディレクトリは前回 保存されたディレクトリが存在する場合はそのディレクトリ名を利用。
        /// 詳細は <a href="../doc/detail.htm#BeforDownloadPath">前回利用したダウンロードホルダの記憶</a>参照。
        /// </remarks>
        protected void SetDefaultDownloadPath()
        {
            string beforDownloadPath = GetSavedDownloadPathName();
            string myDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string downloadPath = null;

            try
            {
                // 前回利用したディレクトリが存在する場合
                if ((! string.IsNullOrEmpty(beforDownloadPath)) &&
                    Directory.Exists(beforDownloadPath))
                {
                    downloadPath = beforDownloadPath;
                }
                else
                {
                    // マイドキュメントの中にディレクトリを作成してそのディレクトリを利用する。
                    downloadPath = Path.Combine(myDocumentsPath, "FileDownload");
                    if (!Directory.Exists(downloadPath))
                    {
                        Directory.CreateDirectory(downloadPath);
                    }
                }
            }
            catch
            {
                // ディレクトリ作成に失敗した場合はマイドキュメントディレクトリを利用する。
                downloadPath = myDocumentsPath;
            }
            this.textDownloadPath.Text = downloadPath;
        }

        /// <summary>
        /// 今回利用したダウンロードディレクトリの情報を保存しておく。
        /// </summary>
        /// <param name="strDir">保存するディレクトリ名</param>
        protected void SaveDownloadPathName(string strDir)
        {
            Properties.Settings.Default.BeforDownloadPath = strDir;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 保存してあるダウンロードパス名を取得する。
        /// ディレクトリ名が未設定の場合には 空文字列が返る。
        /// ユーザが意図してディレクトリを設定するまでは空の文字が返る。
        /// </summary>
        /// <remarks>
        /// もしアプリケーションがバージョンUP等で現在の
        /// Properties.Settings.Default から設定が見つからない場合には
        /// 過去のバージョンで利用している値を取得する。
        /// <see cref="System.Configuration.ApplicationSettingsBase.GetPreviousVersion"/>
        /// <code source="..\FileDownload\DownloadForm.cs"
        /// region="GetSavedDownloadPathName_Code" title="実際のコード" />
        /// </remarks>
        /// <returns>保存されているダウンロードディレクトリ名</returns>
        #region GetSavedDownloadPathName_Code
        protected string GetSavedDownloadPathName()
        {
            string dir = Properties.Settings.Default.BeforDownloadPath;
            if (string.IsNullOrEmpty(dir))
            {
                dir = Properties.Settings.Default.GetPreviousVersion("BeforDownloadPath") as string;
                if (dir != null)
                {
                    Properties.Settings.Default.BeforDownloadPath = dir;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    dir = "";
                }
            }
            return dir;
        }
        #endregion

        /// <summary>
        /// ダウンロード先のURLを変更するためのボタンを押された場合の処理。
        /// <para>パスを入力</para>
        /// <para>指定のパスをダウンロードパスとして指定</para>
        /// </summary>
        /// <remarks>
        /// 処理詳細
        /// <list type="table">
        /// <item>
        /// <description>
        /// ダウンロード先のURLを取得するためのURLをユーザに表示する
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// ダウンロード先を変更する
        /// </description></item>
        /// <item>
        /// <description>
        /// 指定のダウンロード先を <see cref="OpenURLList"/> 関数でダウンロード先に指定する。
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <param name="sender">イベントの送信元オブジェクト</param>
        /// <param name="e">イベントハンドラ</param>
        protected void buttonChangeDownloadPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK) {
                this.textDownloadPath.Text = dlg.SelectedPath;
                OpenURLList(this.textDownloadPath.Text);

                SaveDownloadPathName(this.textDownloadPath.Text);
            }
        }

        /// <summary>
        /// ダウンロード先のディレクトリを指定する。
        /// </summary>
        /// <param name="dirName">ダウンロード先のディレクトリ名</param>
        protected void OpenURLList(string dirName)
        {
            if (Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            string fileName = Path.Combine(dirName, "filelist.txt");
            this.SelectedFileName = fileName;
            if (File.Exists(fileName))
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    this.listURLList.Items.Clear();
                    string data = sr.ReadLine();
                    while (data != null)
                    {
                        this.listURLList.Items.Add(data);

                        data = sr.ReadLine();
                    }
                }
            }
        }

        /// <summary>
        /// 現在のファイルリスト名
        /// </summary>
        private string SelectedFileName ;

        /// <summary>
        /// ダウンロードURL一覧をファイル一覧に保存する。
        /// </summary>
        /// <remarks>
        /// 保存するファイル名は SelectedFileName 変数に格納されている。
        /// </remarks>
        protected void InvokeSaveToURLList()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(this.InvokeSaveToURLList));
                return;
            }
            using (StreamWriter sw = new StreamWriter(SelectedFileName, false))
            {
                foreach (string urlItem in this.listURLList.Items)
                {
                    sw.WriteLine(urlItem);
                }
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// ダウンロードのURLを登録する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddUrl_Click(object sender, EventArgs e)
        {
            // 事前に別ページにリダイレクトするか？確認する
            string url = this.textDownloadUrl.Text.Trim();
            if (url.Length == 0)
            {
                MessageBox.Show("URLを入力してください。");
                return;
            }

            System.Net.HttpWebResponse webres = null;

            try
            {
                System.Net.HttpWebRequest webreq =
                    (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                webres = (System.Net.HttpWebResponse)webreq.GetResponse();
                if (webres.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string addURL = webres.ResponseUri.AbsoluteUri;
                    if (!this.listURLList.Items.Contains(addURL))
                    {
                        this.listURLList.Items.Add(addURL);
                        InvokeSaveToURLList();
                        this.textDownloadUrl.Text = "";
                    }
                }
            }
            catch (System.Net.WebException exp)
            {
                MessageBox.Show(exp.Message);
            }
            catch (Exception exp)
            {
                MessageBox.Show("システムエラー：" + exp.Message);
            }
            finally
            {
                if (webres != null)
                {
                    webres.Close();
                    webres = null;
                }
            }
        }

        private void DownloadForm_Shown(object sender, EventArgs e)
        {
            StartDownloadThread();
        }

        /// <summary>
        /// ダウンロードのスレッドを開始する。
        /// </summary>
        protected void StartDownloadThread()
        {
            downTherad = new Thread(new ThreadStart(this.DownloadLoop));
            downTherad.IsBackground = true;
            downTherad.Start();
        }

        Thread downTherad;

        /// <summary>
        /// ダウンロードのメインループ
        /// </summary>
        /// <remarks>
        /// 基本的な動作：
        /// <para>
        /// ダウンロードはメインスレッドと別のスレッドで行っているため、メインスレッド上にある
        /// フォームの値を取得する場合には注意が必要。
        /// </para>
        /// <para>
        /// 画面上にリストアップされているURLを<see cref="InvokeGetDownloadPath"/>で取得する。
        /// </para>
        /// <para>
        /// <see cref="DownloadFile"/>関数でファイルをダウンロードする。
        /// </para>
        /// <para>
        /// ダウンロードが完了したら画面上に表示されているURLを削除
        /// <see cref="InvokeRemoveURL"/>してダウンロード一覧のファイル一覧を保存
        /// <see cref="InvokeSaveToURLList"/>する。
        /// </para>
        /// <para>
        /// ダウンロードに失敗した場合にはダウンロード中のファイルを一番最後にリストアップする。
        /// <see cref="InvokeAddURL"/>
        /// </para>
        /// </remarks>
        protected void DownloadLoop()
        {
            for (; ; )
            {
                string url = InvokeGetURL();
                string downloadToPath = InvokeGetDownloadPath();
                try
                {
                    if (url != null && url.Length > 0)
                    {
                        DownloadFile(url, downloadToPath);
                        InvokeRemoveURL(url);
                        InvokeSaveToURLList();
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception exp)
                {
                    WriteDownloadUrlLog(Path.Combine(downloadToPath, "error.txt"), 
                        url + "\r\n" + exp.ToString());
                    // ダウンロードエラーの場合は現在ダウンロードを最後に回して次のダウンロードを行う。
                    InvokeRemoveURL(url);
                    InvokeAddURL(url);
                    Debug.WriteLine(exp.ToString());
                }

                System.Threading.Thread.Sleep(1000);
            }
        }


        /// <summary>
        /// バックグラウンドスレッドでファイルのダウンロードを行う
        /// </summary>
        /// <param name="url">ダウンロードするURL</param>
        /// <param name="downloadToPath">ダウンロード先のディレクトリ名</param>
        protected void DownloadFile(string url, string downloadToPath)
        {
            string downloadFileName = Path.GetFileName(url);
            string downloadFileFullName = Path.Combine(downloadToPath, downloadFileName);
            string downloadTempName = downloadFileFullName + ".tmp";

            //If-Rangeヘッダに渡すエンティティタグを指定するときは指定する
            string entityTag = "";

            //WebRequestの作成
            System.Net.HttpWebRequest webreq =
                (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            //ファイルがあればダウンロードが途中であると判断し、
            //ファイルサイズを取得する
            long firstPos;
            if (System.IO.File.Exists(downloadTempName))
                firstPos = (new System.IO.FileInfo(downloadTempName)).Length;
            else
                firstPos = 0;
            if (firstPos > 0)
            {
                //バイトレンジを指定する
                webreq.AddRange((int)firstPos);
                //If-Rangeヘッダを追加
                if (! string.IsNullOrEmpty(entityTag))
                    webreq.Headers.Add("If-Range", entityTag);
            }
            //そのほかのヘッダを指定する
            webreq.KeepAlive = false;
            webreq.Headers.Add("Pragma", "no-cache");
            webreq.Headers.Add("Cache-Control", "no-cache");

            System.Net.HttpWebResponse webres = null;

            try
            {
                //サーバーからの応答を受信するためのWebResponseを取得
                webres = (System.Net.HttpWebResponse)webreq.GetResponse();
            }
            catch (System.Net.WebException e)
            {
                //HTTPプロトコルエラーを捕捉し、内容を表示する
                if (e.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    System.Net.HttpWebResponse errres =
                        (System.Net.HttpWebResponse)e.Response;
                    Console.WriteLine(errres.StatusCode);
                    Console.WriteLine(errres.StatusDescription);
                }
                else
                    Console.WriteLine(e.Message);

                webres.Close();
                throw new ApplicationException("データ取得エラー", e);
            }

            //エンティティタグの表示
            Console.WriteLine("ETag:" + webres.GetResponseHeader("ETag"));

            System.IO.Stream strm = null;
            System.IO.FileStream fs = null;
            try
            {
                //応答データを受信するためのStreamを取得
                strm = webres.GetResponseStream();

                //ファイルに書き込むためのFileStreamを作成
                fs = new System.IO.FileStream(downloadTempName,
                        System.IO.FileMode.OpenOrCreate,
                        System.IO.FileAccess.Write);

                //ファイルに書き込む位置を決定する
                //206Partial Contentステータスコードが返された時はContent-Rangeヘッダを調べる
                //それ以外のときは、先頭から書き込む
                long seekPos = 0;
                if (webres.StatusCode == System.Net.HttpStatusCode.PartialContent)
                {
                    string contentRange = webres.GetResponseHeader("Content-Range");
                    System.Text.RegularExpressions.Match m =
                        System.Text.RegularExpressions.Regex.Match(
                            contentRange,
                        @"bytes\s+(?:(?<first>\d*)-(?<last>\d*)|\*)/(?:(?<len>\d+)|\*)");
                    if (string.IsNullOrEmpty(m.Groups["first"].Value))
                        seekPos = 0;
                    else
                        seekPos = int.Parse(m.Groups["first"].Value);
                }
                else
                {
                    // すでにファイルがある場合にはファイルを削除
                    fs.Close();
                    fs.Dispose();
                    File.Delete(downloadTempName);
                    fs = new System.IO.FileStream(downloadTempName,
                        System.IO.FileMode.OpenOrCreate,
                        System.IO.FileAccess.Write);
                }
                // 途中からのダウンロードにあわせて 書き込み開始位置を変更する
                fs.SetLength(seekPos);
                fs.Position = seekPos;

                //応答データをファイルに書き込む
                byte[] readData = new byte[1024 * 10];
                int readSize = 0;
                long readDataLength = 0;
                for (; ; )
                {
                    readSize = strm.Read(readData, 0, readData.Length);
                    if (readSize == 0)
                        break;
                    fs.Write(readData, 0, readSize);
                    readDataLength += readSize;
                    this.InvokeDownloadStatus(url, readDataLength.ToString() + "/" + webres.ContentLength.ToString());
                }
            }

            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (strm != null)
                {
                    strm.Close();
                    strm.Dispose();
                }
                if (webres != null)
                {
                    webres.Close();
                }
            }
            // ダウンロードファイルの移動
            string bakFile = downloadFileFullName + ".bak";
            if (File.Exists(bakFile)) {
                File.Delete(bakFile);
            }
            if (File.Exists(downloadFileFullName))
            {
                File.Move(downloadFileFullName, bakFile);
            }
            File.Move(downloadTempName, downloadFileFullName);

            this.InvokeDownloadStatus(url, "complate");

            WriteDownloadUrlLog(downloadToPath + "\\complate.log" ,url);
        }

        /// <summary>
        /// ダウンロード完了したURLをログに出力する。
        /// </summary>
        /// <param name="filename">ログのファイル名</param>
        /// <param name="url">ダウンロード完了したURL</param>
        protected static void WriteDownloadUrlLog(string filename,string url)
        {
            using (StreamWriter sw = new StreamWriter(filename, true))
            {
                sw.WriteLine(url);
                sw.Flush();
                sw.Close();
            }
        }

        delegate string GetStringDelegate();

        delegate void DownloadStatusDelegate(string url, string downloadByte);

        /// <summary>
        /// ダウンロードの進行状況をステータスバーに表示する。（この関数は別スレッドからでも値を設定できる）
        /// </summary>
        /// <param name="url">ダウンロード中のURL</param>
        /// <param name="downloadByte">ダウンロードしたバイト数（「ダウンロード済み／全体」の書式で設定する）</param>
        protected void InvokeDownloadStatus(string url, string downloadByte)
        {
            if (this.InvokeRequired)
            {
                DownloadStatusDelegate func = new DownloadStatusDelegate(this.InvokeDownloadStatus);
                this.Invoke(func, new object[] { url, downloadByte });
                return;
            }

            this.toolStripStatusLabel1.Text = url;
            this.toolStripStatusLabel2.Text = downloadByte;
        }

        /// <summary>
        /// ダウンロード先を取得する。（この関数は別スレッドからでも値を取得できる）
        /// </summary>
        /// <returns>ダウンロード先のURLとして登録されているディレクトリ</returns>
        protected string InvokeGetDownloadPath()
        {
            if (this.InvokeRequired)
            {
                GetStringDelegate func = new GetStringDelegate(this.InvokeGetDownloadPath);
                return (string) this.Invoke(func);
            }
            return this.textDownloadPath.Text;
        }


        /// <summary>
        /// ダウンロードする対象のURLを取得する。（この関数は別スレッドからでも値を取得できる）
        /// </summary>
        /// <returns>URL一覧に登録されているURL
        /// URLが1件も登録されていない場合には 空文字列が帰る</returns>
        protected string InvokeGetURL()
        {
            if (this.InvokeRequired)
            {
                GetStringDelegate func = new GetStringDelegate(this.InvokeGetURL);
                return (string)this.Invoke(func);
            }
            if (this.listURLList.Items.Count > 0)
            {
                string url = this.listURLList.Items[0].ToString();
                //this.listBox1.Items.RemoveAt(0);
                return url;
            }
            return "";
        }

        delegate void AddRemoveURLDelegate(string url);

        /// <summary>
        /// ダウンロード一覧に URL を追加する。（この関数は別スレッドからでも値を設定できる）
        /// </summary>
        /// <param name="url">ダウンロードURL</param>
        public void InvokeAddURL(string url)
        {
            if (this.InvokeRequired)
            {
                AddRemoveURLDelegate func = new AddRemoveURLDelegate(InvokeAddURL);
                this.Invoke(func, new object[] { url });
                return;
            }
            this.listURLList.Items.Add(url);
        }

        /// <summary>
        /// URL一覧の中から指定のURLを削除する。（この関数は別スレッドからでも値を設定できる）
        /// </summary>
        /// <param name="url">指定のURL</param>
        protected void InvokeRemoveURL(string url)
        {
            if (this.InvokeRequired)
            {
                AddRemoveURLDelegate func = new AddRemoveURLDelegate(InvokeRemoveURL);
                this.Invoke(func, new object[] { url });
                return;
            }
            this.listURLList.Items.Remove(url);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (this.textDownloadUrl.Text.Length > 0)
            {
                this.buttonAddUrl.Enabled = true;
            }
            else
            {
                this.buttonAddUrl.Enabled = false;
            }
        }
    }
}