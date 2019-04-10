public static void Main(string [] args){
  string url = args[0];
  string requestFile = args[1];
  string[] request = null;

  using (StreamReader rdr = new StreamReader(File.OpenRead(requestFile)))
  request = rdr.ReadToEnd().Split('\n');

  string json = request[request.Length -1];
  JObject obj = JObject.Parse(json);

  Console.Writeline("Fuzzing this frickin' Post request to URL " + url);
  IterateAndFuzz(url,obj);
}

// loop over paired values
private static void IterateAndFuzz(string url, JObject obj){
  foreach(var pair in (JObject)obj.Deepclone()){
    Console.Writeline == JTokenType.String || pair.Value.Type == JTokenType.Integer){

      if (pair.Value.Type == JTokenType.Integer)
      Console.Writeline("Converting int type to string to fuzz");

      JTokenType oldVal = pair.Value;
      obj[pair.Key] = pair.Value.ToString() + "''";

      if (Fuzz(url,obj.Root))
      Console.Writeline("SQL injection vector: " + pair.Key);
      else
      Console.Writeline(pair.Key + " not vulnerable. ");

      obj[pair.Key] = oldVal;
    }
  }
}

private static bool Fuzz(string url, JToken obj){
  byte[] data = System.Text.Encoding.ASCII.GetBytes(obj.ToString());

  HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
  req.Method = "POST";
  req.ContentLength = data.Length;
  req.ContentType = "application/javascript";
  using (Stream stream = req.GetRequestStream())
  stream.Write(data, 0, data.length);

  try{
    req.GetResponse();
  }
  catch(WebException e){
    string resp = string.Empty;
    using (StreamReader r = new StreamReader(e.Response.GetResponseStream()))
    resp = r.ReadToEnd();
    return (resp.Contains("syntax error") || resp.Contains.("unterminated"));
  }
  return false;
}