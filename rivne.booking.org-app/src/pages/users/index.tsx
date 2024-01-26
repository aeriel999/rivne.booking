import { BsFillPencilFill, BsFillTrashFill } from 'react-icons/bs';
import { useActions } from '../../hooks/useActions.ts';
import { useEffect, useState } from 'react';
import { useTypedSelector } from '../../hooks/useTypedSelector.ts';
import { IUser } from '../../interfaces/user';
import DeleteUserModal from '../users/delete/index.tsx';
import DefaultAvatar from '../../images/user/default-avatar.png';
import { useNavigate } from 'react-router-dom';
import Breadcrumb from '../../components/Breadcrumb.tsx';

const Users: React.FC = () => {
  const { GetAll, DeleteUser } = useActions();
  const { allUsers, user } = useTypedSelector((store) => store.UserReducer);
  const [selectedUser, setSelectedUser] = useState(null);
  const [data, setData] = useState<IUser[]>([]);
  // @ts-ignore
  const BASE_URL: string = import.meta.env.VITE_API_URL as string;
  const navigate = useNavigate();

  useEffect(() => {
    GetAll();
  }, []);

  useEffect(() => {
    // Update the 'data' state when 'allUsers' changes
    setData(allUsers.map((user: any) => ({
      id: user.id,
      firstname: user.firstName,
      lastname: user.lastName,
      email: user.email,
      emailConfirmed: user.emailConfirmed,
      phonenumber: user.phoneNumber,
      phoneNumberConfirmed: user.phoneNumberConfirmed,
      lockoutEnabled: user.lockoutEnabled,
      role: user.role,
      avatar: user.avatar === null || user.avatar === '' ? DefaultAvatar : BASE_URL + "/images/avatars/" + user.avatar

    })));

  }, [allUsers]);

  // Function to delete a row
  const openDeleteModal = (model: any) => {
    setSelectedUser(model);
  };

  const closeDeleteModal = () => {
    setSelectedUser(null);
  };

  const confirmDeleteUser = async (userId: any) => {
    await DeleteUser(userId);

    closeDeleteModal();

    setData((data) => data.filter((x) => x.id !== userId));
  };

  // Function to edit a row
  const editRow = (userId: any) => {

    navigate(`/dashboard/users/edit/${userId}`);
    //navigate(`/dashboard/users/edit/`);

  };

  return (
    <div
      className="rounded-sm border border-stroke bg-white px-5 pt-6 pb-2.5 shadow-default dark:border-strokedark dark:bg-boxdark sm:px-7.5 xl:pb-1">
      <Breadcrumb pageName="All users" />

      <div className="max-w-full overflow-x-auto table-wrapper">
        <table className="table">
          <thead>
          <tr className="bg-gray-2 text-left dark:bg-meta-4">
            <th className="min-w-[220px] py-4 px-4 font-medium text-black dark:text-white xl:pl-11">Avatar</th>
            <th className="min-w-[150px] py-4 px-4 font-medium text-black dark:text-white">Name</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Lastname</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Email</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Email confirmed</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Phone number</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Phone number confirmed</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Role</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Is active</th>
            <th className="py-4 px-4 font-medium text-black dark:text-white">Actions</th>
          </tr>
          </thead>
          <tbody>
          {data.map((row: any, idx: number) => {


            return (
              <tr key={idx} className="content-center">
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <div className="h-12.5 w-15 rounded-md">
                    <img src={row.avatar} alt="User" />
                  </div>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.firstname}
                  </span>
                </td>

                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.lastname}
                  </span>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>{row.email}</span>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.emailConfirmed.toString()}
                  </span>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.phonenumber}
                  </span>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.phoneNumberConfirmed.toString()}
                  </span>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.role}
                  </span>
                </td>
                <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
                  <span>
                    {row.lockoutEnabled.toString()}
                  </span>
                </td>
                {row.id !== user.Id ? (
                  <td className="border-b border-[#eee] py-5 px-4 dark:border-strokedark">
      <span className="actions flex grid-cols-2 gap-4">
        <BsFillTrashFill
          className="delete-btn cursor-pointer"
          onClick={() => openDeleteModal(row)}
        />
        <BsFillPencilFill
          className="edit-btn cursor-pointer"
          onClick={() => editRow(row.id)}
        />
      </span>
                  </td>
                ) : null}
              </tr>
            );
          })}
          </tbody>

        </table>

        {selectedUser && (
          <DeleteUserModal
            // @ts-ignore
            user={selectedUser}
            onCancel={closeDeleteModal}
            onConfirm={confirmDeleteUser}
          />
        )}
      </div>
    </div>

  );
};

export default Users;