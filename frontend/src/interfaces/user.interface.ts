export interface IRegistrationModel {
  login: string,
  email: string,
  password: string,
  confirmPassword: string,
  confirmPolice: boolean
}
export interface IRegistration {
  login: string,
  email: string,
  password: string,
  confirmPassword: string,
}

export interface IAuthorization {
  email: string;
  password: string;
}

export interface IUserData {
  id: number;
  email: string;
  name: string;
  phone: string;

}
