export interface IRegistrationModel {
  login: string;
  email: string;
  password: string;
  confirmPassword: string;
  confirmPolice: boolean;
}

export interface IRegistration {
  email: string;
  password: string;
  confirmPassword: string;
}

export interface IAuthorization {
  email: string;
  password: string;
  rememberMe?: boolean;
}

export interface IUserData {
  id: number;
  email: string;
  name: string;
  phone: string;
  img: string;
  dateConnection: string;
  courseComplete: string;
  login?: string;
}

export interface IChangePassword {
  currentPassword: string;
  newPassword: string;
  confirmNewPassword: string;
}

export interface IChangeEmail {
  newEmail: string;
}
