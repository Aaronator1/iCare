package just_hacking_array_android.app.SignInAccountCommand;


import just_hacking_array_android.app.Incoding.JsonModelStateData;

public interface ISignInAccountCommandListener {
	void Success(SignInAccountCommandResponse response);	
	void Error(JsonModelStateData[] modelState);
}