import { useNavigate, useParams } from 'react-router-dom';
import { useActions } from '../../../hooks/useActions.ts';
import { useEffect, useState } from 'react';
import { IEditApartment, IStreet } from '../../../interfaces/apartment';
import { useTypedSelector } from '../../../hooks/useTypedSelector.ts';
import { arrowSVG, checkBoxSVG, roleSVG } from '../../../images/icon/user.tsx';
import { areaSVG, buildingSVG, floorSVG, priceSVG, roomsSVG, streetsSVG } from '../../../images/icon/apartments.tsx';
import { useFormik } from 'formik';
import Breadcrumb from '../../../components/Breadcrumb.tsx';
import InputGroup from '../../../components/InputGroup.tsx';
import { addApartmentsValidationSchema } from '../../../validation/apartment';
import { GetProp, UploadFile, UploadProps } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { Modal, Upload } from 'antd';
import EditorTiny from '../../../components/TinyEditor';



type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

const getBase64 = (file: FileType): Promise<string> =>
  new Promise((resolve, reject) => {
    const reader = new FileReader();
    // @ts-ignore
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result as string);
    reader.onerror = (error) => reject(error);
  });

const convertUploadFileToFile = (uploadFile: UploadFile): File | null => {
  // Check if the necessary properties are available
  if (uploadFile.originFileObj && uploadFile.name) {
    // Create a new File object
    const file = new File([uploadFile.originFileObj as Blob], uploadFile.name, {
      type: uploadFile.type,
      lastModified: uploadFile.lastModified,
    });

    return file;
  }

  return null;
};

const UpdateApartment : React.FC = () => {
  const { apartmentId } = useParams();
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();
  const  { GetStreetsList, GetApartment, EditApartment} = useActions();
  const {streetsList, selectedApartment} = useTypedSelector((store)=>store.ApartmentReducer)
  const [streets, setStreets] = useState<IStreet[]>([]);
  const [previewOpen, setPreviewOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState('');
  const [previewTitle, setPreviewTitle] = useState('');
  const [fileList, setFileList] = useState<UploadFile[]>([ ]);
  const [isChecked, setIsChecked] = useState<boolean>(false);
  const [typesOfBooking, setTypesOfBooking] = useState([]);
  // @ts-ignore
  const BASE_URL: string = import.meta.env.VITE_API_URL as string;
  const imgUrl = BASE_URL + "/images/apartments/";
  const [imageUrls, setImageUrls] = useState<string[]>([]);
  const [imagesForDelete, setImagesForDelete] = useState<string[]>([]);


  useEffect(() => {
    GetStreetsList();

    const fetchData = async () => {
      console.log("apartmentId", apartmentId)
      // @ts-ignore
      await  GetApartment(apartmentId)

      setIsLoading(false);
    };

    fetchData();
  }, [apartmentId]);



  useEffect(() => {
    setStreets(streetsList.map((street: any)=>(
      {
        id: street.id,
        name: street.name
      }
    )))
  }, [streetsList]);


  useEffect(() => {
    // Your logic to fetch roles goes here
    const fetchedTypesOfBooking = ['For hour', 'For day', 'For month'];
    // @ts-ignore
    setTypesOfBooking(fetchedTypesOfBooking);
  }, []);


  const initialValues: IEditApartment = {
    id: null,
    numberOfBuilding: null,
    numberOfRooms: null,
    isPrivateHouse: null,
    isBooking: null,
    isActive: null,
    isArchived: null,
    typeOfBooking: "",
    description: "",
    userName: "",
    streetName: null,
    streetId: null,
    userId: null,
    rooms: null,
    floor: null,
    area: null,
    price: null,
    images:  null,
    existImages: null,
    imagesForDelete: null,
    existStreetName: ""
  };

  const handleCancel = () => setPreviewOpen(false);

  const handlePreview = async (file: UploadFile) => {
    if (!file.url && !file.preview) {
      file.preview = await getBase64(file.originFileObj as FileType);
    }

    setPreviewImage(file.url || (file.preview as string));
    setPreviewOpen(true);
    setPreviewTitle(file.name || file.url!.substring(file.url!.lastIndexOf('/') + 1));
  };

  const handleImageChange: UploadProps['onChange'] = ({ fileList: newFileList }) =>
    setFileList(newFileList);

  const uploadButton = (
    <button style={{ border: 0, background: 'none' }} type="button">
      <PlusOutlined />
      <div style={{ marginTop: 8 }}>Upload</div>
    </button>
  );



  const handleDeleteImage = (index: number) => {
    // Get the image name from the imageUrl
    const imageName = imageUrls[index].split('/').pop() || '';

    // Add the image name to the imagesForDelete list
    setImagesForDelete((prevImages) => [...prevImages, imageName]);
    // Remove the image URL and update the state
    const updatedUrls = [...imageUrls];
    updatedUrls.splice(index, 1);
    setImageUrls(updatedUrls);
  };

  const onFormikSubmit = async (values: IEditApartment) => {
    try {
      // Convert each UploadFile to File
      const files: File[] = fileList.map((uploadFile) => {
        const file = convertUploadFileToFile(uploadFile);
        return file as File;
      });

      const model: IEditApartment = {
        ...values,
        images: files,
        imagesForDelete: imagesForDelete
      };

      console.log("model: IEditApartment", model)
      await EditApartment(model);

      navigate('/dashboard/apartments');
    } catch (error) {
      console.error('Form submission failed:', error);
    }
  };


  const formik = useFormik({
    initialValues: initialValues,
    onSubmit: onFormikSubmit,
    validationSchema: addApartmentsValidationSchema
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
    if (selectedApartment) {

     // setIsChecked(selectedUser.lockoutEnabled || false);
      setFieldValue('id', selectedApartment.id);
      setFieldValue('numberOfBuilding', selectedApartment.numberOfBuilding);
      setFieldValue('numberOfRooms', selectedApartment.numberOfRooms);
      setFieldValue('isPrivateHouse', selectedApartment.isPrivateHouse);
      setFieldValue('isBooking', selectedApartment.isBooking);
      setFieldValue('isActive', selectedApartment.isActive);
      setFieldValue('isArchived', selectedApartment.isArchived);
      setFieldValue('typeOfBooking', selectedApartment.typeOfBooking);
      setFieldValue('description', selectedApartment.description);
      setFieldValue('userName', selectedApartment.userName);
      setFieldValue('userId', selectedApartment.userId);
      setFieldValue('existStreetName', selectedApartment.streetName);
      setFieldValue('streetId', selectedApartment.streetId);
      setFieldValue('rooms', selectedApartment.rooms);
      setFieldValue('floor', selectedApartment.floor);
      setFieldValue('area', selectedApartment.area);
      setFieldValue('price', selectedApartment.price);
     // setFieldValue('images', selectedApartment.images);
      console.log("selectedApartment.images", selectedApartment.images)
      const urls = selectedApartment.images?.map((image : any) => image) || [];
      setImageUrls(urls);
console.log("urls", urls)
    }
  }, [selectedApartment]);
  const handleCancelClick = () => {
    navigate('/dashboard/apartments');
  };

  console.log("selectedApartment", selectedApartment)

  if (isLoading) {
    return <p>Loading...</p>;
  }

  return (
    <>
      <div className="mx-auto max-w-270">

        <Breadcrumb pageName="Edit Apartment" />

        <div className="grid grid-cols-5 gap-8">
          <div className="col-span-5 xl:col-span-3">
            <div
              className="rounded-sm border border-stroke bg-white shadow-default dark:border-strokedark dark:bg-boxdark">

              <div className="p-7">

                <form onSubmit={handleSubmit}>

                  <div className="mb-5.5">
                    <div className="flex flex-col gap-5.5 p-6.5">
                      <div>
                        <label className="mb-3 block text-black dark:text-white">
                          Select Street
                        </label>
                        <div className="relative z-20 bg-white dark:bg-form-input">
                  <span className="absolute top-1/2 left-4 z-30 -translate-y-1/2">
                   {streetsSVG()}
                  </span>
                          <select
                            className="relative z-20 w-full appearance-none rounded border border-stroke bg-transparent py-3 px-12 outline-none transition focus:border-primary active:border-primary dark:border-form-strokedark dark:bg-form-input"
                            name="streetId"
                            onChange={(e) => setFieldValue('streetId', e.target.value)}
                          >
                            <option value="">{values.existStreetName}</option>
                            {streets.map((street, index) => (
                              <option key={index} value={street.id}>
                                {street.name}
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

                  <div className="mb-5.5">
                    <InputGroup
                      label="Or input Street Name if you didn`n find it in List"
                      field="streetName"
                      onChange={handleChange}
                      error={errors.streetName}
                      touched={touched.streetName}
                      icon={streetsSVG()}
                    />
                  </div>

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
                          name="isPrivateHouse"
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
                      It is a Private House
                    </label>
                  </div>

                  <div className="mb-5.5 flex flex-col gap-5.5 sm:flex-row">
                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Number Of Rooms"
                        field="numberOfRooms"
                        onChange={handleChange}
                        type="number"

                        value={values.numberOfRooms}
                        error={errors.numberOfRooms}
                        touched={touched.numberOfRooms}
                        icon={roomsSVG()}
                      />


                    </div>
                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Number Of Building"
                        field="numberOfBuilding"
                        onChange={handleChange}
                        type="number"

                        value={values.numberOfBuilding}
                        error={errors.numberOfBuilding}
                        touched={touched.numberOfBuilding}
                        icon={buildingSVG()}
                      />
                    </div>
                  </div>

                  <div className="mb-5.5 flex flex-col gap-5.5 sm:flex-row">
                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Floor"
                        field="floor"
                        onChange={handleChange}
                        type="number"

                        value={values.floor}
                        error={errors.floor}
                        touched={touched.floor}
                        icon={floorSVG()}
                      />


                    </div>
                    <div className="w-full sm:w-1/2">
                      <InputGroup
                        label="Area"
                        field="area"
                        onChange={handleChange}
                        type="number"

                        value={values.area}
                        error={errors.area}
                        touched={touched.area}
                        icon={areaSVG()}
                      />
                    </div>
                  </div>

                  <div className="mb-5.5">
                    <div className="flex flex-col gap-5.5 p-6.5">
                      <div>

                        <div className="relative z-20 bg-white dark:bg-form-input">
                  <span className="absolute top-1/2 left-4 z-30 -translate-y-1/2">
                   {roleSVG()}
                  </span>
                          <select
                            className="relative z-20 w-full appearance-none rounded border border-stroke bg-transparent py-3 px-12 outline-none transition focus:border-primary active:border-primary dark:border-form-strokedark dark:bg-form-input"
                            name="typeOfBooking" // Don't forget to specify the name attribute to capture the selected value
                            onChange={(e) => setFieldValue('typeOfBooking', e.target.value)}
                          >
                            <option value="">{values.typeOfBooking} </option>
                            {typesOfBooking.map((type, index) => (
                              <option key={index} value={type}>
                                {type}
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

                  <div className="mb-5.5">
                    <InputGroup
                      label="Price"
                      field="price"
                      onChange={handleChange}
                      type="number"

                      value={values.price}
                      error={errors.price}
                      touched={touched.price}
                      icon={priceSVG()}
                    />
                  </div>

                  <div className="mb-5.5">

                    <EditorTiny
                      value={values.description} //Значення, яке ми вводимо в поле
                      label="Description" //Підпис для даного інпуту
                      field="description" //Назва інпуту
                      error={errors.description} //Якщо є помилка, то вона буде передаватися
                      touched={touched.description} //Якщо натискалася кнопка Submit
                      onEditorChange={(text: string) => {
                        //Метод, який викликає сам компонет, коли в інпуті змінюється значення
                        setFieldValue("description", text); //Текст, який в середині інпуту, записуємо у формік в поле description
                      }}
                    />
                  </div>

                  {/*<div className="mb-5.5">*/}
                  {/*  <Upload*/}
                  {/*    // actions="https://run.mocky.io/v3/435e224c-44fb-4773-9faf-380c5e6a2188"*/}
                  {/*    beforeUpload={() => false}*/}
                  {/*    listType="picture-card"*/}
                  {/*    fileList={fileList}*/}
                  {/*    multiple*/}
                  {/*    onPreview={handlePreview}*/}
                  {/*    onChange={handleImageChange}*/}
                  {/*  >*/}
                  {/*    {fileList.length >= 8 ? null : uploadButton}*/}
                  {/*  </Upload>*/}
                  {/*  <Modal open={previewOpen} title={previewTitle} footer={null} onCancel={handleCancel}>*/}
                  {/*    <img alt="example" style={{ width: '100%' }} src={previewImage} />*/}
                  {/*  </Modal>*/}
                  {/*</div>*/}


                  <div className="mb-5.5">
                    <Upload
                      beforeUpload={() => false}
                      listType="picture-card"
                      fileList={fileList}
                      multiple
                      onPreview={handlePreview}
                      onChange={handleImageChange}
                    >
                      {fileList.length >= 8 ? null : uploadButton}
                    </Upload>
                    <Modal open={previewOpen} title={previewTitle} footer={null} onCancel={handleCancel}>
                      <img alt="example" style={{ width: '100%' }} src={previewImage} />
                    </Modal>
                  </div>

                  {/* Render existing images */}
                  <div className="mb-5.5">
                    {imageUrls.map((imageUrl, index) => (
                      <div key={index} style={{ marginBottom: '10px', position: 'relative' }}>
                        <img src={imgUrl + imageUrl} alt={`image-${index}`} style={{ maxWidth: '100%' }} />
                        <button
                          className="absolute top-0 right-0 bg-red-500 text-white p-1 cursor-pointer"
                          onClick={() => handleDeleteImage(index)}
                          type="button"
                        >
                          Delete
                        </button>
                      </div>
                    ))}
                  </div>

                  {/*Buttons*/}
                  <div className="flex justify-end gap-4.5">
                    <button
                      className="flex justify-center rounded border border-stroke py-2 px-6 font-medium text-black hover:shadow-1 dark:border-strokedark dark:text-white"
                      onClick={handleCancelClick}
                      type="button"
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

export default UpdateApartment;