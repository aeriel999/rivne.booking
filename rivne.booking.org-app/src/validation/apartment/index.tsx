import * as yup from 'yup';

export const addApartmentsValidationSchema = yup.object({
  numberOfBuilding: yup.number().required('Number of building is required'),
  numberOfRooms: yup.number().required('Number of rooms is required'),
  floor: yup.number().required('Number of floor is required'),
  area: yup.number().required('Area is required'),
  price: yup.number().required('Price is required'),
  description: yup.string().required('Description is required'),
});