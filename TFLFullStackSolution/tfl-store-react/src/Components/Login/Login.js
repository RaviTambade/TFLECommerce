
import './Login.css';

function Login() {

  return (
    <div>
      <h2>Login</h2>
        <form>
        <label for="email">Email:</label><br/>
                <input type="email" id="email" name="email" value="ravi.tambade@transdflower.in"/><br/>
                <label for="lname">Last name:</label><br/>
                <input type="password" id="password" name="passwird" /><br/><br/>
                <input type="submit" value="Submit"/>
        </form> 
    </div>
  );
}

export default Login;
