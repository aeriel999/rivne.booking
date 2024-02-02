
interface ApartmentCardProps {
  id: number;
  userName: string;
  postDate: string;
  updateDate: string;
  isActive: boolean;
  isBooking: boolean;
  address: string;
  typeOfBooking: string;
  rooms: number;
  floor: number;
  area: number;
  price: number;
  image: string;
}
const ApartmentCard: React.FC<ApartmentCardProps> = (props) => {


  const cardStyle: React.CSSProperties = {
    border: '1px solid #ccc',
    borderRadius: '8px',
    margin: '10px',
    overflow: 'hidden',
    boxShadow: '0 2px 4px rgba(0, 0, 0, 0.1)',
  };

  const headerStyle: React.CSSProperties = {
    backgroundColor: '#f8f8f8',
    padding: '10px',

  };

  const contentStyle: React.CSSProperties = {
    padding: '15px',
  };
  const headersStyle: React.CSSProperties = {
    fontWeight: 'bold'
  };

  return (
    <>
      {/*<div*/}
      {/*  className="rounded-sm border border-stroke bg-white py-6 px-7.5 shadow-default dark:border-strokedark dark:bg-boxdark">*/}
        <div className="mb-5.5 flex flex-col gap-5.5 sm:flex-row">
          <div className="w-full sm:w-1/4">
            <img
              src={props.image} />
          </div>
          <div className="w-full sm:w-3/4">
            <div style={cardStyle} className="mb-5.5 flex flex-col gap-5.5 sm:flex-row">
              <div style={headerStyle}>
                <div className="flex flex-row gap-5.5">
                  <p style={headersStyle}>User: </p>
                  <p>{props.userName}</p>
                </div>
                <div className="flex flex-row gap-5.5">
                  <p style={headersStyle}>Posted date: </p>
                  <p>{props.postDate}</p>
                </div>
                <div className="flex flex-row gap-5.5">
                  <p style={headersStyle}>Updated date: </p>
                  <p>{props.updateDate}</p>
                </div>
                <div className="flex flex-row gap-5.5">
                  <p style={headersStyle}>Moderated: </p>
                  <p>{props.isActive === true ? "Yes" : "No"}</p>
                </div>
                <div className="flex flex-row gap-5.5">
                  <p style={headersStyle}>Has a reservation: </p>
                  <p>{props.isBooking === true ? "Yes" : "No"}</p>
                </div>
              </div>
              <div style={contentStyle} className="card-content">
                <p>Address: {props.address}</p>
                <p>Rooms: {props.rooms}</p>
                <p>Floor: {props.floor}</p>
                <p>Area: {props.area} sq. ft.</p>
                <p>Price: ${props.price}/{props.typeOfBooking}</p>
                {/* Add more details as needed */}
              </div>
            </div>
          </div>
        </div>
      {/*  <span className="actions flex grid-cols-2 gap-4">*/}
      {/*  <BsFillTrashFill*/}
      {/*    className="delete-btn cursor-pointer"*/}
      {/*    // onClick={() => openDeleteModal(row)}*/}
      {/*  />*/}
      {/*  <BsFillPencilFill*/}
      {/*    className="edit-btn cursor-pointer"*/}
      {/*    // onClick={() => editRow(row.id)}*/}
      {/*  />*/}
      {/*</span>*/}
      {/*</div>*/}

    </>
  );
};

export default ApartmentCard;