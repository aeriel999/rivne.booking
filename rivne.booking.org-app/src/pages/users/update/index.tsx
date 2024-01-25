import { useNavigate, useParams } from 'react-router-dom';
import { useTypedSelector } from '../../../hooks/useTypedSelector.ts';
import { useActions } from '../../../hooks/useActions.ts';
import { useEffect, useState } from 'react';
import Breadcrumb from '../../../components/Breadcrumb.tsx';
import { IEditUser } from '../../../interfaces/user';
import { editUserValidationSchema } from '../../../validation/user';
import { useFormik } from 'formik';
import InputGroup from '../../../components/InputGroup.tsx';
import { arrowSVG, checkBoxSVG, emailSVG, phoneSVG, roleSVG, userSVG } from '../../../images/icon/user.tsx';


const EditUserPage: React.FC = () => {
  const { userId } = useParams();
  const navigate = useNavigate();
  const { GetUser, EditUser } = useActions();
  const { selectedUser } = useTypedSelector((store) => store.UserReducer);
  const [isLoading, setIsLoading] = useState(true);
  const [isChecked, setIsChecked] = useState<boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      // @ts-ignore
      await GetUser(userId);
      setIsLoading(false);
    };

    fetchData();
  }, [userId]);

  const initialValues: IEditUser = {
    id: '',
    firstname: '',
    lastname: '',
    email: '',
    phonenumber: '',
    role: '',
    lockoutEnabled: false
  };

  const onFormikSubmit = async (values: IEditUser) => {
    console.log('values  ', values);

    try {
      const model: IEditUser = {
        ...values,
        id: selectedUser.id,
        lockoutEnabled: isChecked
      };
      console.log('model after', model);
      await EditUser(model);

      navigate('/dashboard/users');
    } catch (error) {
      console.error('Form submission failed:', error);
    }
  };

  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onFormikSubmit,
    validationSchema: editUserValidationSchema
  });

  const {
    values,
    touched,
    errors,
    handleSubmit,
    handleChange,
    setFieldValue
  } = formik;

  useEffect(() => {
    if (selectedUser) {
      console.log('selectedUser', selectedUser);
      setIsChecked(selectedUser.lockoutEnabled || false);
      setFieldValue('id', selectedUser.id);
      setFieldValue('firstname', selectedUser.firstName);
      setFieldValue('lastname', selectedUser.lastName);
      setFieldValue('email', selectedUser.email);
      setFieldValue('phonenumber', selectedUser.phoneNumber);
      setFieldValue('role', selectedUser.role);
    }
  }, [selectedUser]);

  const handleCancelClick = () => {
    navigate('/dashboard/users');
  };

  if (isLoading) {
    return <p>Loading...</p>;
  }
  return (
    <>
      <div className="mx-auto max-w-270">

        <Breadcrumb pageName="Edit User" />

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
                  {/*//Name + Lastneme*/}
                  <div className="mb-5.5 flex flex-col gap-5.5 sm:flex-row">
                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Name"
                        field="firstname"
                        value={values.firstname}
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
                        value={values.lastname}
                        onChange={handleChange}
                        error={errors.lastname}
                        touched={touched.lastname}
                        icon={userSVG()}
                      />
                    </div>
                  </div>

                  {/*//email + confirmed*/}
                  <div className="mb-5.5">
                    <InputGroup
                      label="Email address"
                      field="email"
                      value={values.email}
                      onChange={handleChange}
                      error={errors.email}
                      touched={touched.email}
                      icon={emailSVG()}
                    />
                  </div>

                  {/*        <div className="w-full sm:w-1/3">*/}
                  {/*          <label*/}
                  {/*            className="mb-3 block text-sm font-medium text-black dark:text-white"*/}
                  {/*            htmlFor="emailaddress"*/}
                  {/*          >*/}
                  {/*            Comfirmed*/}
                  {/*          </label>*/}

                  {/*          /!*Switcher*!/*/}
                  {/*          <div>*/}
                  {/*            <label*/}
                  {/*              htmlFor="toggle3"*/}
                  {/*              className="flex cursor-pointer select-none items-center"*/}
                  {/*            >*/}
                  {/*              <div className="relative">*/}
                  {/*                <input*/}
                  {/*                  type="checkbox"*/}
                  {/*                  id="toggle3"*/}
                  {/*                  className="sr-only"*/}
                  {/*                  checked={emailEnabled}*/}
                  {/*                  onChange={() => {*/}
                  {/*                    setEmailEnabled(!emailEnabled);*/}

                  {/*                  }}*/}
                  {/*                />*/}
                  {/*                <div className="block h-8 w-14 rounded-full bg-meta-9 dark:bg-[#5A616B]"></div>*/}
                  {/*                <div*/}
                  {/*                  className={`dot absolute left-1 top-1 flex h-6 w-6 items-center justify-center rounded-full bg-white transition ${*/}
                  {/*                    emailEnabled && '!right-1 !translate-x-full !bg-primary dark:!bg-white'*/}
                  {/*                  }`}*/}
                  {/*                >*/}
                  {/*<span className={`hidden ${emailEnabled && '!block'}`}>*/}
                  {/*  <svg*/}
                  {/*    className="fill-white dark:fill-black"*/}
                  {/*    width="11"*/}
                  {/*    height="8"*/}
                  {/*    viewBox="0 0 11 8"*/}
                  {/*    fill="none"*/}
                  {/*    xmlns="http://www.w3.org/2000/svg"*/}
                  {/*  >*/}
                  {/*    <path*/}
                  {/*      d="M10.0915 0.951972L10.0867 0.946075L10.0813 0.940568C9.90076 0.753564 9.61034 0.753146 9.42927 0.939309L4.16201 6.22962L1.58507 3.63469C1.40401 3.44841 1.11351 3.44879 0.932892 3.63584C0.755703 3.81933 0.755703 4.10875 0.932892 4.29224L0.932878 4.29225L0.934851 4.29424L3.58046 6.95832C3.73676 7.11955 3.94983 7.2 4.1473 7.2C4.36196 7.2 4.55963 7.11773 4.71406 6.9584L10.0468 1.60234C10.2436 1.4199 10.2421 1.1339 10.0915 0.951972ZM4.2327 6.30081L4.2317 6.2998C4.23206 6.30015 4.23237 6.30049 4.23269 6.30082L4.2327 6.30081Z"*/}
                  {/*      fill=""*/}
                  {/*      stroke=""*/}
                  {/*      strokeWidth="0.4"*/}
                  {/*    ></path>*/}
                  {/*  </svg>*/}
                  {/*</span>*/}
                  {/*                  <span className={`${emailEnabled && 'hidden'}`}>*/}
                  {/*  <svg*/}
                  {/*    className="h-4 w-4 stroke-current"*/}
                  {/*    fill="none"*/}
                  {/*    viewBox="0 0 24 24"*/}
                  {/*    xmlns="http://www.w3.org/2000/svg"*/}
                  {/*  >*/}
                  {/*    <path*/}
                  {/*      strokeLinecap="round"*/}
                  {/*      strokeLinejoin="round"*/}
                  {/*      strokeWidth="2"*/}
                  {/*      d="M6 18L18 6M6 6l12 12"*/}
                  {/*    ></path>*/}
                  {/*  </svg>*/}
                  {/*</span>*/}
                  {/*                </div>*/}
                  {/*              </div>*/}
                  {/*            </label>*/}
                  {/*          </div>*/}
                  {/*          /!*SwitcherEnd*!/*/}


                  {/*        </div>*/}


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

                  {/*<div className="w-full sm:w-1/3">*/}
                  {/*  <label*/}
                  {/*    className="mb-3 block text-sm font-medium text-black dark:text-white"*/}
                  {/*    htmlFor="phoneNumber"*/}
                  {/*  >*/}
                  {/*    Comfirmed*/}
                  {/*  </label>*/}

                  {/*  /!*Switcher*!/*/}
                  {/*  /!*<div>*!/*/}
                  {/*  /!*  <label*!/*/}
                  {/*  /!*    htmlFor="toggle1"*!/*/}
                  {/*  /!*    className="flex cursor-pointer select-none items-center"*!/*/}
                  {/*  /!*  >*!/*/}
                  {/*  /!*    <div className="relative">*!/*/}
                  {/*  /!*      <input*!/*/}
                  {/*  /!*        type="checkbox"*!/*/}
                  {/*  /!*        id="toggle1"*!/*/}
                  {/*  /!*        className="sr-only"*!/*/}
                  {/*  /!*        checked={phoneEnabled}*!/*/}
                  {/*  /!*        onChange={() => {*!/*/}
                  {/*  /!*          setPhoneEnabled(!phoneEnabled);*!/*/}
                  {/*  /!*        }}*!/*/}
                  {/*  /!*      />*!/*/}
                  {/*  /!*      <div className="block h-8 w-14 rounded-full bg-meta-9 dark:bg-[#5A616B]"></div>*!/*/}
                  {/*  /!*      <div*!/*/}
                  {/*  /!*        className={`dot absolute left-1 top-1 flex h-6 w-6 items-center justify-center rounded-full bg-white transition ${*!/*/}
                  {/*  /!*          phoneEnabled && '!right-1 !translate-x-full !bg-primary dark:!bg-white'*!/*/}
                  {/*  /!*        }`}*!/*/}
                  {/*  /!*      >*!/*/}
                  {/*  /!*<span className={`hidden ${phoneEnabled && '!block'}`}>*!/*/}
                  {/*  /!*  <svg*!/*/}
                  {/*  /!*    className="fill-white dark:fill-black"*!/*/}
                  {/*  /!*    width="11"*!/*/}
                  {/*  /!*    height="8"*!/*/}
                  {/*  /!*    viewBox="0 0 11 8"*!/*/}
                  {/*  /!*    fill="none"*!/*/}
                  {/*  /!*    xmlns="http://www.w3.org/2000/svg"*!/*/}
                  {/*  /!*  >*!/*/}
                  {/*  /!*    <path*!/*/}
                  {/*  /!*      d="M10.0915 0.951972L10.0867 0.946075L10.0813 0.940568C9.90076 0.753564 9.61034 0.753146 9.42927 0.939309L4.16201 6.22962L1.58507 3.63469C1.40401 3.44841 1.11351 3.44879 0.932892 3.63584C0.755703 3.81933 0.755703 4.10875 0.932892 4.29224L0.932878 4.29225L0.934851 4.29424L3.58046 6.95832C3.73676 7.11955 3.94983 7.2 4.1473 7.2C4.36196 7.2 4.55963 7.11773 4.71406 6.9584L10.0468 1.60234C10.2436 1.4199 10.2421 1.1339 10.0915 0.951972ZM4.2327 6.30081L4.2317 6.2998C4.23206 6.30015 4.23237 6.30049 4.23269 6.30082L4.2327 6.30081Z"*!/*/}
                  {/*  /!*      fill=""*!/*/}
                  {/*  /!*      stroke=""*!/*/}
                  {/*  /!*      strokeWidth="0.4"*!/*/}
                  {/*  /!*    ></path>*!/*/}
                  {/*  /!*  </svg>*!/*/}
                  {/*  /!*</span>*!/*/}
                  {/*  /!*        <span className={`${phoneEnabled && 'hidden'}`}>*!/*/}
                  {/*  /!*  <svg*!/*/}
                  {/*  /!*    className="h-4 w-4 stroke-current"*!/*/}
                  {/*  /!*    fill="none"*!/*/}
                  {/*  /!*    viewBox="0 0 24 24"*!/*/}
                  {/*  /!*    xmlns="http://www.w3.org/2000/svg"*!/*/}
                  {/*  /!*  >*!/*/}
                  {/*  /!*    <path*!/*/}
                  {/*  /!*      strokeLinecap="round"*!/*/}
                  {/*  /!*      strokeLinejoin="round"*!/*/}
                  {/*  /!*      strokeWidth="2"*!/*/}
                  {/*  /!*      d="M6 18L18 6M6 6l12 12"*!/*/}
                  {/*  /!*    ></path>*!/*/}
                  {/*  /!*  </svg>*!/*/}
                  {/*  /!*</span>*!/*/}
                  {/*  /!*      </div>*!/*/}
                  {/*  /!*    </div>*!/*/}
                  {/*  /!*  </label>*!/*/}
                  {/*  /!*</div>*!/*/}
                  {/*  /!*SwitcherEnd*!/*/}

                  {/*</div>*/}

                  {/*Role Coosing*/}
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
                            name="role"
                            defaultValue={selectedUser.role}
                            onChange={(e) => setFieldValue('role', e.target.value)}
                          >
                            {selectedUser.roles.map((role: string, index: number) => (
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

                  {/*User Activation*/}
                  <div className="mb-5.5">
                    <label
                      htmlFor="checkboxLabelTwo"
                      className="flex cursor-pointer select-none items-center"
                    >
                      <div className="relative">
                        <input
                          type="checkbox"
                          id="checkboxLabelTwo"
                          className="sr-only"
                          name="lockoutEnabled"
                          checked={isChecked}
                          onChange={() => {
                            setIsChecked(!isChecked);
                          }}
                        />
                        <div
                          className={`mr-4 flex h-5 w-5 items-center justify-center rounded border ${
                            isChecked && 'border-primary bg-gray dark:bg-transparent'
                          }`}
                        >
            <span className={`opacity-0 ${isChecked && '!opacity-100'}`}>
            {checkBoxSVG()}
            </span>
                        </div>
                      </div>
                      User Activation
                    </label>
                  </div>

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

export default EditUserPage;


