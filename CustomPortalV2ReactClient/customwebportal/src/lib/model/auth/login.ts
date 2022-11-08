export interface LoginRequest {   
    UserName: string;
    Password: string;
  }

export interface LoginReturn{
    isLogin:boolean;
    token:string;
}
  