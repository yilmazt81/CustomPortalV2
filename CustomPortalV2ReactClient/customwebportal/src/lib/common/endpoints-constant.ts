

const env = "http://localhost:44395/";

class Auth {  
    static base = env; 
    static login =env +'api/login';

}
class User { 
    static   login = env + 'api/user';
  }

  export class Endpoint {
    static   AUTH = Auth;
    static   User = User;
   // static readonly USERS = Users;
    //static readonly TASKS = Tasks;
  }