using Microsoft.AspNetCore.NodeServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Web.Common
{
    /// <summary>
    /// 公共类
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 通过Google.cn 翻译
        /// </summary>
        /// <param name="text">需要翻译的文本</param>
        /// <param name="fromLang">需要翻译的文本语言。自动:auto</param>
        /// <param name="toLang">目标语言。英文:en</param>
        /// <param name="nodeServices">nodeServices</param>
        /// <returns></returns>
        public static async Task<string> Translate(string text, string fromLang, string toLang, INodeServices nodeServices)
        {
            CookieContainer cc = new CookieContainer();
            string GoogleTransBaseUrl = "https://translate.google.cn/";
            var BaseResultHtml = GetResultHtml(GoogleTransBaseUrl, cc, "");
            Regex re = new Regex(@"(?<=tkk:')(.*?)(?=')");
            string tkk = re.Match(BaseResultHtml).ToString();//在返回的HTML中正则匹配TKK的值  
            string tk = await nodeServices.InvokeAsync<string>("./wwwroot/js/gettk", text, tkk);
            string googleTransUrl = "https://translate.google.cn/translate_a/single?client=t&sl=" + fromLang + "&tl=" + toLang + "&hl=en&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&ie=UTF-8&oe=UTF-8&otf=1&ssel=0&tsel=0&kc=1&tk=" + tk + "&q=" + HttpUtility.UrlEncode(text);
            var ResultHtml = GetResultHtml(googleTransUrl, cc, "https://translate.google.cn/");
            dynamic TempResult = Newtonsoft.Json.JsonConvert.DeserializeObject(ResultHtml);
            string ResultText = Convert.ToString(TempResult[0][0][0]);
            return ResultText;
        }
        /// <summary>
        /// 获取页面代码内容
        /// </summary>
        /// <param name="url">页面Url地址路径</param>
        /// <param name="cookie">cookie容器</param>
        /// <param name="referer">referer header</param>
        /// <returns></returns>
        private static string GetResultHtml(string url, CookieContainer cookie, string referer)
        {
            var html = "";
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.CookieContainer = cookie;
            webRequest.Referer = referer;
            webRequest.Timeout = 20000;
            webRequest.Headers.Add("X-Requested-With:XMLHttpRequest");
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36";
            using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
            {
                using (var reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                {
                    html = reader.ReadToEnd();
                    reader.Close();
                    webResponse.Close();
                }
            }
            return html;
        }
        /// <summary>
        /// 语种
        /// </summary>
        public enum Language
        {
            [Description("简体中文")]
            zh_CN = 0,
            [Description("English")]
            en_US = 1,
            [Description("繁體中文")]
            zh_Hant = 2,
            [Description("韩语")]
            ko_KR = 3,
            [Description("日本語")]
            ja_JP = 4,
            [Description("ภาษาไทย")]
            th_TH = 5,
            [Description("Deutsch")]
            de_DE = 6,
            [Description("Français")]
            fr_FR = 7,
            [Description("西班牙语")]
            es_ES = 8,
            [Description("意大利语")]
            it_IT = 9
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }
    }
}
