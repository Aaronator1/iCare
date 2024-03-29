package just_hacking_array_android.app.Incoding;


import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;
import org.apache.http.Header;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;

import org.apache.http.impl.client.DefaultHttpClient;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.Map.Entry;

public class IncodingHelper {


	public static String ToValue(Object ob) throws UnsupportedEncodingException {
		if(ob instanceof java.util.Date)
			return new SimpleDateFormat("dd/MM/yyyy").format(ob);
				
		return String.valueOf(ob);
	}
	
	
    public static void Verify(JSONObject result) throws JSONException, ModelStateException {
        if (!result.getBoolean("success")) {
            JSONArray errors = result.isNull("data") ? new JSONArray() : result.getJSONArray("data");
            JsonModelStateData[] state = new JsonModelStateData[errors.length()];
            for (int i = 0; i < errors.length(); i++) {
                JSONObject itemError = errors.getJSONObject(i);
                JsonModelStateData jsonModelStateData = new JsonModelStateData();
                jsonModelStateData.errorMessage = itemError.getString("errorMessage");
                jsonModelStateData.isValid = itemError.getBoolean("isValid");
                jsonModelStateData.name = itemError.getString("name");
                state[i] = jsonModelStateData;
            }
            throw new ModelStateException(state);
        }
    }

    public static java.util.Date getDate(String dateAsString) throws ParseException {
        return new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss").parse(dateAsString);
    }

    public static HttpResponse Execute(Context context, boolean isPost, String type, HashMap<String,Object> parameters) throws IOException {
    	String url = "http://justhackingarray.incframework.com/Dispatcher" + "/" + type;
        SharedPreferences preferences = PreferenceManager.getDefaultSharedPreferences(context);
        HttpResponse response;

            HttpGet http = new HttpGet(url + "?" + getQuery(parameters));
            http.setHeader("Cookie", preferences.getString("Set-Cookie", "Set-Cookie"));
            http.setHeader("X-Requested-With", "XMLHttpRequest");
            response = new DefaultHttpClient().execute(http);

        Header[] cookies = response.getHeaders("Set-Cookie");
        if (cookies != null && cookies.length != 0) {
            SharedPreferences.Editor edit = preferences.edit();
            String combineCookie = preferences.getString("Set-Cookie", "Set-Cookie");
            for (Header header : cookies)
                combineCookie += header.getValue() + ";";
            edit.putString("Set-Cookie", combineCookie);
            edit.commit();
        }

        return response;
    }

    private static String getQuery(HashMap<String,Object> params) throws UnsupportedEncodingException {
        StringBuilder result = new StringBuilder();
        boolean first = true;

        for(Entry<String, Object> entry : params.entrySet()) {
            if (first)
                first = false;
            else
                result.append("&");

            result.append(URLEncoder.encode(entry.getKey(), "UTF-8"));
            result.append("=");
            result.append(URLEncoder.encode(ToValue(entry.getValue()), "UTF-8"));
        }

        return result.toString();
    }


}