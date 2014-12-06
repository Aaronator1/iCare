package just_hacking_array_android.app.SignInAccountCommand;


import org.json.JSONException;
import org.json.JSONObject;	
import java.text.ParseException;
import org.json.JSONArray;

import just_hacking_array_android.app.Incoding.IncodingHelper;
import just_hacking_array_android.app.Incoding.ModelStateException;

public class SignInAccountCommandResponse {
    public Object data;

    public static SignInAccountCommandResponse Create(JSONObject result) throws JSONException, ModelStateException, ParseException {
	    IncodingHelper.Verify(result);
        SignInAccountCommandResponse response = new SignInAccountCommandResponse();
        response.data = result.isNull("data") ? null : result.get("data");
        return response;
    }
 	                                                           
}