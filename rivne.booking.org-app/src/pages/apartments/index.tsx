import ApartmentCard from '../../components/AppartmentCard.tsx';
import { useActions } from '../../hooks/useActions.ts';
import { useEffect, useState } from 'react';
import { useTypedSelector } from '../../hooks/useTypedSelector.ts';
import { IApartment } from '../../interfaces/apartment';
import NoImage from '../../images/product/No_image.png';
import Breadcrumb from '../../components/Breadcrumb.tsx';
import { BsFillPencilFill, BsFillTrashFill } from 'react-icons/bs';
import DeleteApartmentModal from './delete';
import { useNavigate } from 'react-router-dom';


const Apartments: React.FC = () => {
  const { GetAllApartments, DeleteApartment } = useActions();
  const { allApartment } = useTypedSelector((store) => store.ApartmentReducer);
  const [data, setData] = useState<IApartment[]>([]);
  const[selectedApartments, setSelectedApartments] = useState(null);
  // @ts-ignore
  const BASE_URL: string = import.meta.env.VITE_API_URL as string;
  const navigate = useNavigate();
  useEffect(() => {
    GetAllApartments();
  }, []);

  useEffect(() => {
    setData(allApartment.map((apartament: any) => ({
      id: apartament.id,
      userName: apartament.userName,
      postDate: apartament.dateOfPost,
      updateDate: apartament.dateOfUpdate,
      isActive: apartament.isPosted,
      isBooking: apartament.isBooked,
      typeOfBooking: apartament.typeOfBooking,
      address: apartament.address,
      rooms: apartament.numberOfRooms,
      floor: apartament.floor,
      area: apartament.area,
      price: apartament.price,
      image: apartament.image === null ? NoImage : BASE_URL + "/images/apartments/" + apartament.image
    })));
  }, [allApartment]);

  const openDeleteModal = (model: any) => {
    setSelectedApartments(model);
  };
  const closeDeleteModal = () => {
    setSelectedApartments(null);
  };

  const confirmDeleteUser = async (apartmentId: any) => {

    await DeleteApartment(apartmentId);

    closeDeleteModal();

    setData((data) => data.filter((x) => x.id !== apartmentId));
  };

console.log("List data", data)

  const editRow = (apartmentId: any) => {

    navigate(`/dashboard/apartment/edit/${apartmentId}`);

  };

  return (
    <>
      <div
        className="rounded-sm border border-stroke bg-white px-5 pt-6 pb-2.5 shadow-default dark:border-strokedark dark:bg-boxdark sm:px-7.5 xl:pb-1">
        <Breadcrumb pageName="All Apartaments" />
        {data.map((apartment) => (
          <div
            key={apartment.id} // Explicitly set the key prop using the unique id
            className="rounded-sm border border-stroke bg-white py-6 px-7.5 shadow-default dark:border-strokedark dark:bg-boxdark"
          >
            {/*// @ts-ignore*/}
            <ApartmentCard {...apartment} />
            <span className="actions flex grid-cols-2 gap-4">
      <BsFillTrashFill
        className="delete-btn cursor-pointer"
        onClick={() => openDeleteModal(apartment)}
      />
      <BsFillPencilFill
        className="edit-btn cursor-pointer"
         onClick={() => editRow(apartment.id)}
      />
    </span>
          </div>
        ))}

        {selectedApartments && (
          <DeleteApartmentModal
            // @ts-ignore
            apartment={selectedApartments}
            onCancel={closeDeleteModal}
            onConfirm={confirmDeleteUser}
          />
        )}
      </div>
    </>
  );
};

export default Apartments;