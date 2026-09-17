using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Common.AudionSystem
{
    /// <summary>
    /// Audioソースの名前定数クラス作成ツール
    /// UnityエディタのTools>Create>AudioNamePathから実行可能
    /// 作成先: Assets/Scripts/Common/AudioSystem/AUDIO.cs
    ///
    /// AUDIO.cs内に各AudioのPathが定数として定義されるため、そちらを参照してAudioを再生する
    /// </summary>
    public static class AudioNamePathCreator
    {
        /// <summary>
        /// Audioファイルの名前定数クラスを作成するコマンド名
        /// Audioファイルの出力先の指定
        /// </summary>
        private const string CommandName = "Tools/Creat/AudioNamePath";
        private const string ExportPath = "Assets/Scripts/Common/AudioSystem/AUDIO.cs";

        /// <summary>
        /// ファイル名と拡張子なしのファイル名を取得
        /// </summary>
        private static readonly string Filename = Path.GetFileName(ExportPath);
        private static readonly string FilenameWithoutExtension = Path.GetFileNameWithoutExtension(ExportPath);


        [MenuItem(CommandName)]
        public static void CreateAudioName()
        {
            CreateScript();
            EditorUtility.DisplayDialog("Audio名の定数クラスを作成しました", $"ファイル名: {Filename}", "OK");
        }


        /// <summary>
        /// スクリプトの作成
        /// </summary>
        private static void CreateScript()
        {
            var builder = new StringBuilder();

            builder.AppendLine("/// <summary>");
            builder.AppendLine("/// Audio名を定数で管理するクラス");
            builder.AppendLine("/// </summary>");
            builder.AppendFormat("public static class {0}", FilenameWithoutExtension).AppendLine();
            builder.AppendLine("{");

            //指定したパスのリソースを全て取得
            var bgmList = Resources.LoadAll($"Audio/BGM");
            var seList = Resources.LoadAll($"Audio/SE");

            foreach (var o in bgmList)
            {
                var bgm = (AudioClip)o;
                builder.Append("\t")
                    .AppendFormat(@"  public const string BGM_{0} = ""{1}"";", bgm.name.ToUpper(), bgm.name)
                    .AppendLine();
            }

            builder.AppendLine("\t");

            foreach (var o in seList)
            {
                var se = (AudioClip)o;
                builder.Append("\t")
                    .AppendFormat(@"  public const string SE_{0} = ""{1}"";", se.name.ToUpper(), se.name).AppendLine();
            }

            builder.AppendLine("\t");
            builder.AppendLine("}");

            var directoryName = Path.GetDirectoryName(ExportPath);
            if (!Directory.Exists(directoryName))
            {
                if (directoryName != null) Directory.CreateDirectory(directoryName);
            }

            File.WriteAllText(ExportPath, builder.ToString(), Encoding.UTF8);
            AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
        }
    }
}