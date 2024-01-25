import * as yup from "yup";

const passwordRegExp =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-_]).{6,}$/;
export const profileValidationSchema = yup.object({
  email: yup.string().email('Invalid email').required('Email is required'),
  firstname: yup.string().min(3, "Name must have at least 3 symbols").max(24, "Name must be less than 24 symbols"),
  lastname: yup.string().min(3, "Lastname must have at least 3 symbols").max(24, "Lastname must be less than 24 symbols"),
  phonenumber: yup.string().min(11, "Phone number must have at least 11 symbols")
});

export const editUserValidationSchema = yup.object({
  email: yup.string().email('Invalid email').required('Email is required'),
  firstname: yup.string().min(3, "Name must have at least 3 symbols").max(24, "Name must be less than 24 symbols"),
  lastname: yup.string().min(3, "Lastname must have at least 3 symbols").max(24, "Lastname must be less than 24 symbols"),
  phonenumber: yup.string().min(11, "Phone number must have at least 11 symbols"),
  role: yup.string().required('Role is required'),
});

export const addUserValidationSchema = yup.object({
  email: yup.string().email('Invalid email').required('Email is required'),
  firstname: yup.string().min(3, "Name must have at least 3 symbols").max(24, "Name must be less than 24 symbols"),
  lastname: yup.string().min(3, "Lastname must have at least 3 symbols").max(24, "Lastname must be less than 24 symbols"),
  phonenumber: yup.string().min(11, "Phone number must have at least 11 symbols"),
  role: yup.string().required('Role is required'),
  password: yup.string()
    .max(255)
    .min(6)
    .required("Password is required")
    .matches(passwordRegExp, "Password must contains A-Z, a-z, 0-9"),
  confirmPassword: yup.string()
    .max(255)
    .min(6)
    .required("Confirm password is required")
    .matches(passwordRegExp, "Password must contains A-Z, a-z, 0-9")
    .oneOf([yup.ref("password")], "Passwords must match."),
});
