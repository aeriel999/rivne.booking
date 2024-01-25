import { useNavigate } from 'react-router-dom';
import { useActions } from '../../../hooks/useActions.ts';
import { useEffect, useState } from 'react';
import Breadcrumb from '../../../components/Breadcrumb.tsx';
import { IAddUser } from '../../../interfaces/user';
import { useFormik } from 'formik';
import { addUserValidationSchema } from '../../../validation/user';
import InputGroup from '../../../components/InputGroup.tsx';
import { arrowSVG, emailSVG, passwordSVG, phoneSVG, roleSVG, userSVG } from '../../../images/icon/user.tsx';

const AddUserPage: React.FC = () => {
  const navigate = useNavigate();
  const { AddUser } = useActions();
  const [roles, setRoles] = useState([]);

  useEffect(() => {
    // Your logic to fetch roles goes here
    const fetchedRoles = ['Admin', 'User'];
    // @ts-ignore
    setRoles(fetchedRoles);
  }, []);

  const initialValues: IAddUser = {
    firstname: '',
    lastname: '',
    email: '',
    phonenumber: '',
    role: '',
    password: '',
    confirmPassword: ''
  };
  const onFormikSubmit = async (values: IAddUser) => {
    console.log('values  ', values);

    try {
      const model: IAddUser = {
        ...values

      };
      console.log('model after', model);
      await AddUser(model);

      navigate('/dashboard/users');
    } catch (error) {
      console.error('Form submission failed:', error);
    }
  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onFormikSubmit,
    validationSchema: addUserValidationSchema
  });

  const {
    values,
    touched,
    errors,
    handleSubmit,
    handleChange,
    setFieldValue
  } = formik;
  const handleCancelClick = () => {
    navigate('/dashboard/users');
  };
  return (
    <>
      <div className="mx-auto max-w-270">
        <Breadcrumb pageName="Add User" />
        <div className="grid grid-cols-5 gap-8">
          <div className="col-span-5 xl:col-span-3">
            <div
              className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
              <div className="border-b border-stroke py-4 px-7 dark:border-strokedark">
                <h3 className="font-medium text-black dark:text-white">
                  Edit Personal Information
                </h3>
              </div>
              <div className="p-7">
                <form onSubmit={handleSubmit}>

                  {/*//Name + Lastname*/}
                  <div className="mb-5.5 flex flex-col gap-5.5 sm:flex-row">
                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Name"
                        field="firstname"
                        // value={values.firstname}
                        onChange={handleChange}
                        error={errors.firstname}
                        touched={touched.firstname}
                        icon={userSVG()}
                      />
                    </div>

                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Lastname"
                        field="lastname"
                        // value={values.lastname}
                        onChange={handleChange}
                        error={errors.lastname}
                        touched={touched.lastname}
                        icon={userSVG()}
                      />
                    </div>
                  </div>

                  {/*//email  */}
                  <div className="mb-5.5">
                    <InputGroup
                      label="Email address"
                      field="email"
                      // value={values.email}
                      onChange={handleChange}
                      error={errors.email}
                      touched={touched.email}
                      icon={emailSVG()}
                    />
                  </div>

                  {/*//Phonenumber + confirmed*/}
                  <div className="mb-5.5">
                    <InputGroup
                      label="Phone number"
                      field="phonenumber"
                      value={values.phonenumber}
                      onChange={handleChange}
                      error={errors.phonenumber}
                      touched={touched.phonenumber}
                      icon={phoneSVG()}
                    />
                  </div>

                  {/*Role Choosing*/}
                  <div className="mb-5.5">
                    <div className="flex flex-col gap-5.5 p-6.5">
                      <div>
                        <label className="mb-3 block text-black dark:text-white">
                          Select Role
                        </label>
                        <div className="relative z-20 bg-white dark:bg-form-input">
                  <span className="absolute top-1/2 left-4 z-30 -translate-y-1/2">
                   {roleSVG()}
                  </span>
                          <select
                            className="relative z-20 w-full appearance-none rounded border border-stroke bg-transparent py-3 px-12 outline-none transition focus:border-primary active:border-primary dark:border-form-strokedark dark:bg-form-input"
                            name="role" // Don't forget to specify the name attribute to capture the selected value
                            onChange={(e) => setFieldValue('role', e.target.value)}
                          >
                            <option value="">Select a Role</option>
                            {roles.map((role, index) => (
                              <option key={index} value={role}>
                                {role}
                              </option>
                            ))}
                          </select>
                          <span className="absolute top-1/2 right-4 z-10 -translate-y-1/2">
                    {arrowSVG()}
                  </span>
                        </div>
                      </div>
                    </div>
                  </div>
                  {/*Password*/}

                  <div className="mb-4">
                    <InputGroup
                      label="Password"
                      field="password"
                      type="password"
                      onChange={handleChange}
                      error={errors.password}
                      touched={touched.password}
                      icon={passwordSVG()}
                    />
                  </div>

                  {/* Re-type Password*/}
                  <div className="mb-6">
                    <InputGroup
                      label="Confirm password"
                      field="confirmPassword"
                      type="password"
                      onChange={handleChange}
                      error={errors.confirmPassword}
                      touched={touched.confirmPassword}
                      icon={passwordSVG()}
                    />
                  </div>

                  {/*Buttons*/}
                  <div className="flex justify-end gap-4.5">
                    <button
                      className="flex justify-center rounded border border-stroke py-2 px-6 font-medium text-black hover:shadow-1 dark:border-strokedark dark:text-white"
                      onClick={handleCancelClick}
                    >
                      Cancel
                    </button>
                    <button
                      className="flex justify-center rounded bg-primary py-2 px-6 font-medium text-gray hover:shadow-1"
                      type="submit"
                    >
                      Save
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default AddUserPage;


