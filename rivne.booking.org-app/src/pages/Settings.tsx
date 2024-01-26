import Breadcrumb from '../components/Breadcrumb';
import DefaultAvatar from '../images/user/default-avatar.png';
import { useTypedSelector } from '../hooks/useTypedSelector.ts';
import { IAvatar, IProfileUser } from '../interfaces/user';
import { useActions } from '../hooks/useActions.ts';
import { useNavigate } from 'react-router-dom';
import {   useFormik } from 'formik';
import { profileValidationSchema } from '../validation/user';
import InputGroup from '../components/InputGroup.tsx';
import { emailSVG, phoneSVG, uploadArrowSVG, userSVG } from '../images/icon/user.tsx';
import { useState } from 'react';
//mport { File } from 'typescript';


const Settings = () => {
  const { user } = useTypedSelector((store) => store.UserReducer);
  // @ts-ignore
  const BASE_URL: string = import.meta.env.VITE_API_URL as string;
  const avatar = user.Avatar === '' ? DefaultAvatar : BASE_URL + "/images/avatars/" + user.Avatar;

  console.log(avatar);
  const { UpdateUserProfile, AddUserAvatar, LogOut } = useActions();
  const navigate = useNavigate();
  const [selectedFile, setSelectedFile] = useState(null);

  const initialValues : IProfileUser = {
    id: user.id,
    firstname: user.Firstname || '',
    lastname: user.Lastname || '',
    email: user.Email,
    phonenumber: user.PhoneNumber || ''
  };

  // @ts-ignore
  const onFormikSubmit  = async (values: IProfileUser) => {
   // console.log("values", values)
    try {
      // Perform the form submission or validation logic here
      const model: IProfileUser = {
        ...values,
        id: user.Id
      };

      await UpdateUserProfile(model);

      // Navigate to the desired page after successful form submission
      LogOut(user.Id);
    } catch (error) {
      console.error('Form submission failed:', error);
    }
  };

  const formik = useFormik({
    initialValues: initialValues, //початкові налаштування для полів
    onSubmit: onFormikSubmit , //метод, який спрацьовує, коли усі дані у форміку валідні
    validationSchema: profileValidationSchema, //схема валідації даних
  });

  const {
    values,
    touched,
    errors,
    handleSubmit,
    handleChange,
   // setFieldValue,
  } = formik;
//const OnClick = ()=>{};
const OnClickCancel = ()=>{
  navigate('/dashboard');
};

  const onFileChange = (e : any) => {

    setSelectedFile(e.target.files[0]);
  };

  const onAvatarSubmit = async (e: any) => {
    e.preventDefault();

    if (!selectedFile) {
      console.log("no file is selected");
      return;
    }

    console.log("selectedFile", selectedFile)

    const model: IAvatar = {
      id: user.Id,
      image: selectedFile
    }

    console.log("model", model)

   try{
     await AddUserAvatar(model);

     LogOut(user.Id);
   } catch (error) {
     console.error('Form submission failed:', error);
   }
  };

  return (
    <>
      <div className="mx-auto max-w-270">

        <Breadcrumb pageName="Settings" />

        <div className="grid grid-cols-5 gap-8">
          <div className="col-span-5 xl:col-span-3">
            <div className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
              <div className="border-b border-stroke py-4 px-7 dark:border-strokedark">
                <h3 className="font-medium text-black dark:text-white">
                  Personal Information
                </h3>
              </div>
              <div className="p-7">
                <form onSubmit={handleSubmit}>
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

                  <div className="mb-5.5">
                    <InputGroup
                      label="Email address"
                      field="email"
                      value={values.email}
                      onChange={handleChange}
                      error={errors.email}
                      touched={touched.email}
                      icon={emailSVG()}
                      type="email"
                    />
                  </div>

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

                  <div className="flex justify-end gap-4.5">
                    <button
                      className="flex justify-center rounded border border-stroke py-2 px-6 font-medium text-black hover:shadow-1 dark:border-strokedark dark:text-white"
                     onClick={OnClickCancel}
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

          <div className="col-span-5 xl:col-span-2">
            <div
              className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">
              <div className="border-b border-stroke py-4 px-7 dark:border-strokedark">
                <h3 className="font-medium text-black dark:text-white">
                  Your Avatar
                </h3>
              </div>
              <div className="p-7">
                <form onSubmit={onAvatarSubmit}>
                  <div className="mb-4 flex items-center gap-3">
                    <div className="h-14 w-14 rounded-full">
                      <img src={avatar} alt="User" />
                    </div>
                    <div>
                      <span className="mb-1.5 text-black dark:text-white">
                        Edit your photo
                      </span>
                      <span className="flex gap-2.5">
                        {/*<button  onClick={OnClick}  className="text-sm hover:text-primary">*/}
                        {/*  Delete*/}
                        {/*</button>*/}
                      </span>
                    </div>
                  </div>

                  <div
                    id="FileUpload"
                    className="relative mb-5.5 block w-full cursor-pointer appearance-none rounded border-2 border-dashed border-primary bg-gray py-4 px-4 dark:bg-meta-4 sm:py-7.5"
                  >
                    <input
                      type="file"
                      accept="image/*"
                      className="absolute inset-0 z-50 m-0 h-full w-full cursor-pointer p-0 opacity-0 outline-none"
                      onChange={onFileChange}
                    />
                    <div className="flex flex-col items-center justify-center space-y-3">
                      <span
                        className="flex h-10 w-10 items-center justify-center rounded-full border border-stroke bg-white dark:border-strokedark dark:bg-boxdark">
                       {uploadArrowSVG()}
                      </span>
                      <p>
                        <span className="text-primary">Click to upload</span> or
                        drag and drop
                      </p>
                      <p className="mt-1.5">WEB, PNG, JPG or GIF</p>
                      <p>(max, 800 X 800px)</p>
                    </div>
                  </div>

                  <div className="flex justify-end gap-4.5">
                    <button
                      className="flex justify-center rounded border border-stroke py-2 px-6 font-medium text-black hover:shadow-1 dark:border-strokedark dark:text-white"
                     onClick={OnClickCancel}
                    >
                      Cancel
                    </button>
                    <button
                      className="flex justify-center rounded bg-primary py-2 px-6 font-medium text-gray hover:bg-opacity-70"
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

export default Settings;