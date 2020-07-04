using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace ZTn.Json.Editor
{
    sealed class JTokenRoot
    {
        #region >> Fields

        private JToken jTokenValue;

        #endregion

        #region >> Properties

        /// <summary>
        /// Root <see cref="JToken"/> node.
        /// </summary>
        public JToken JTokenValue
        {
            get { return jTokenValue; }
            set { jTokenValue = value; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Constructor using an existing stream to populate the instance.
        /// </summary>
        /// <param name="jsonStream">Source stream.</param>
        public JTokenRoot(Stream jsonStream)
        {
            Load(jsonStream);
        }

        /// <summary>
        /// Constructor using an existing json string to populate the instance.
        /// </summary>
        /// <param name="jsonString">Source string.</param>
        public JTokenRoot(string jsonString)
        {
            Load(jsonString);
        }

        /// <summary>
        /// Constructor using an existing json string to populate the instance.
        /// </summary>
        /// <param name="jToken">Source <see cref="JToken"/>.</param>
        public JTokenRoot(JToken jToken)
        {
            Load(jToken);
        }

        #endregion

        /// <summary>
        /// Initialize using an existing stream to populate the instance.
        /// </summary>
        /// <param name="jsonStream">Source stream.</param>
        public void Load(Stream jsonStream)
        {
            using (var streamReader = new StreamReader(jsonStream))
            {
                Load(streamReader.ReadToEnd());
            }
        }

        /// <summary>
        /// Initialize using an existing json string to populate the instance.
        /// </summary>
        /// <param name="jsonString">Source string.</param>
        public void Load(string jsonString)
        {
            try
            {
                Load(JToken.Parse(jsonString));
            }
            catch { }
        }

        /// <summary>
        /// Initialize using an existing json string to populate the instance.
        /// </summary>
        /// <param name="jToken">Source <see cref="JToken"/>.</param>
        public void Load(JToken jToken)
        {
            jTokenValue = jToken;
        }

        /// <summary>
        /// Save the enclosed <see cref="JToken"/> in an existing stream.
        /// </summary>
        /// <param name="jsonStream">Target stream.</param>
        public void Save(Stream jsonStream)
        {
            if (jTokenValue == null)
            {
                return;
            }

            var json = jTokenValue.ToString().Replace("\r\n","\n");
            DialogResult response = DialogResult.Yes;
            if (string.IsNullOrEmpty(json) || json == "[]")
                response = MessageBox.Show(Application.OpenForms[0], "This wizard is empty, incomplete or incorrect. Do you really want to save it ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (response == DialogResult.Yes)
            {
                using (var streamWriter = new StreamWriter(jsonStream, Encoding.GetEncoding(1252)))
                {
                    streamWriter.Write(json);
                }
            }
        }
    }
}
