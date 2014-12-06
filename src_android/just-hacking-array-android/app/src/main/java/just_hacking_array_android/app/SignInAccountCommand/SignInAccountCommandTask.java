package just_hacking_array_android.app.SignInAccountCommand;

import org.apache.http.HttpResponse;
import org.apache.http.util.EntityUtils;
import org.json.JSONObject;
import android.content.Context;
import android.os.AsyncTask;
import java.util.HashMap;

import just_hacking_array_android.app.Incoding.IncodingHelper;
import just_hacking_array_android.app.Incoding.ModelStateException;

public class SignInAccountCommandTask extends AsyncTask<String, Integer, String> {

    private Context context;

    private ISignInAccountCommandListener listener;
    	
		
	private SignInAccountCommandRequest[] request = new SignInAccountCommandRequest[0];

	public SignInAccountCommandTask(Context context,SignInAccountCommandRequest request)	
	{		
		this(context, new SignInAccountCommandRequest[]{request});
	}

	public SignInAccountCommandTask(Context context,SignInAccountCommandRequest[] request) {
		this.context = context;
		this.request = request;
	}

			
	@Override
    protected void onPostExecute(String s) {
        super.onPostExecute(s);
        try {
            listener.Success( SignInAccountCommandResponse.Create(new JSONObject(s)) );
        } catch (Exception e) {
            e.printStackTrace();
        } catch (ModelStateException e) {
            e.printStackTrace();
        }
    }

	@Override
    protected String doInBackground(String... strings) {
        try {
		   
		    HashMap<String, Object> params = new HashMap<String, Object>();			
			String type = "Push";
			if (request.length == 1) {				
				params = request[0].GetParameters(-1);
			} else {
				type = "Composite";
				for (int index = 0; index < request.length; index++) {
					params.putAll(request[index].GetParameters(index));
				}
			}

            HttpResponse response = IncodingHelper.Execute(context, true, type, params);
			return EntityUtils.toString(response.getEntity());
			        
        } catch (Exception e) {
            e.printStackTrace();
        }
        return "";
    }

    public void On(ISignInAccountCommandListener on)
    {
        listener = on;
        execute();
    }
}