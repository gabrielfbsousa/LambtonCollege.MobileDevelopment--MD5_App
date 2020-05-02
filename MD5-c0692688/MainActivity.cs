using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MD5c0692688
{
    [Activity(Label = "MD5-c0692688", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        private EditText _entryEditText;
        private TextView _origTextView;
        private TextView _hashedTextView;

        private string originalText;
        private string hashedText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Matching the view elements on code
            _entryEditText = FindViewById<EditText>(Resource.Id.editText1);
            _origTextView = FindViewById<TextView>(Resource.Id.textView5);
            _hashedTextView = FindViewById<TextView>(Resource.Id.textView7);

            //Creating the button click handler
            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += delegate
            {
                //Method that will do the conversion
                updateHash();
                _hashedTextView.Text = hashedText;
                _origTextView.Text = originalText;
            };
        }

        public async void updateHash()
        {
            //Creating the url
            string userTypedHash = _entryEditText.Text;
            string url = "http://md5.jsontest.com/?text=" + userTypedHash;

            //Getting the JSON from the URL
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //Getting the response from the URL
            HttpResponseMessage messageResponse = await httpClient.GetAsync(url);
            if (messageResponse != null || messageResponse.IsSuccessStatusCode)
            {
                string content = await messageResponse.Content.ReadAsStringAsync();
                JObject item = JObject.Parse(content);


                _origTextView.Text = (string)item.GetValue("original");
                _hashedTextView.Text = (string)item.GetValue("md5");

            }
            else
            {
                Console.Out.WriteLine("An error happened here!");
            }


        }
    }
}

