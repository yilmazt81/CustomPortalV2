import {Endpoint} from './common/endpoints-constant';
import   {BaseHttpService}  from './common/base-http-service';
import {LoginRequest} from '../lib/model/auth/login'

class AuthService extends BaseHttpService {
  login = (loginRequest: LoginRequest) => {
    console.log(Endpoint.AUTH.login);
    return this.post(Endpoint.AUTH.login, loginRequest);
  };
}

const authService = new AuthService();

export {authService};