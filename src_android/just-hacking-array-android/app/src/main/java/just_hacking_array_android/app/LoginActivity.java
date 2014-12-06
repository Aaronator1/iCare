package just_hacking_array_android.app;

import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import just_hacking_array_android.app.Incoding.JsonModelStateData;
import just_hacking_array_android.app.SignInAccountCommand.ISignInAccountCommandListener;
import just_hacking_array_android.app.SignInAccountCommand.SignInAccountCommandRequest;
import just_hacking_array_android.app.SignInAccountCommand.SignInAccountCommandResponse;
import just_hacking_array_android.app.SignInAccountCommand.SignInAccountCommandTask;


public class LoginActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
    }

    public void SingInCommand(final View view) {
        SignInAccountCommandRequest request = new SignInAccountCommandRequest();
       // EditText userNameBox = (EditText) view.findViewById(R.id.userName);
       // EditText passwordBox = (EditText) view.findViewById(R.id.password);
        String userName = "admin@mail.com";//userNameBox.getText().toString();
        String password = "Diaweb1$";//passwordBox.getText().toString();
        if (userName.equals("") || password.equals("")) {
            Toast.makeText(this, "Please provide credentials", Toast.LENGTH_LONG).show();
        } else {
            request.Email = userName;
            request.Password = password;

            new SignInAccountCommandTask(this, request)
                    .On(new ISignInAccountCommandListener() {
                        @Override
                        public void Success(SignInAccountCommandResponse response) {
                            Button button = (Button) view.findViewById(R.id.loginButton);
                            button.setText("Ready");
                        }

                        @Override
                        public void Error(JsonModelStateData[] state) {
                            Button button = (Button) view.findViewById(R.id.loginButton);
                            button.setText(state[0].name + " / " + state[0].errorMessage);
                        }
                    });
        }
    }
}
