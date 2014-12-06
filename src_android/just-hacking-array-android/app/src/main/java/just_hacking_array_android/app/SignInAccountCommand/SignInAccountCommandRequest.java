package just_hacking_array_android.app.SignInAccountCommand;

public class SignInAccountCommandRequest {

    public String Email;
    public String Password;
     

   public java.util.HashMap<String, Object> GetParameters(Integer index) {
	java.util.HashMap<String, Object> parameters = new java.util.HashMap<String, Object>();	
	parameters.put("incType","SignInAccountCommand");
		 if (this.Email != null)     parameters.put(index == -1 ? "Email" : String.format("[%s].Email", index),this.Email);
    	 if (this.Password != null)     parameters.put(index == -1 ? "Password" : String.format("[%s].Password", index),this.Password);
       
	return parameters;
   } 
                                         
}