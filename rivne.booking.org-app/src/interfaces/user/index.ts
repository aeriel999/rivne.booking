export interface ILogin {
  email: string,
  password: string,
}

export interface IProfileUser {
  id: string,
  firstname: string,
  lastname: string,
  email: string,
  phonenumber: string,
}

export interface IUser {
  id: string,
  firstname: string,
  lastname: string,
  email: string,
  emailConfirmed: boolean,
  phonenumber: string,
  phoneNumberConfirmed: boolean,
  lockoutEnabled: boolean,
  role: string,
  avatar: string,
}

export interface IEditUser {
  id: string,
  firstname: string,
  lastname: string,
  email: string,
 // emailConfirmed: boolean,
  phonenumber: string,
  //phoneNumberConfirmed: boolean,s
  lockoutEnabled: boolean,
  role: string,
}

export interface IAddUser {
  firstname: string,
  lastname: string,
  email: string,
  phonenumber: string,
  role: string,
  password: string,
  confirmPassword: string,
}

