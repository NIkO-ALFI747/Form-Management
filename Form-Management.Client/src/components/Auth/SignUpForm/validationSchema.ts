import * as yup from 'yup';

const validationSchema = yup.object().shape({
  name: yup
    .string()
    .required('Name is required.'),
  email: yup
    .string()
    .required('Email is required.')
    .email('Invalid email format.'),
  password: yup
    .string()
    .required('Password is required.')
    .min(8, 'Password must be at least 8 characters long.')
    .matches(
      /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$/,
      'Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.'
    ),
});

export default validationSchema;
