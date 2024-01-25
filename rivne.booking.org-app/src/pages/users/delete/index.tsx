import React from 'react';

// @ts-ignore
const DeleteUserModal : React.FC = ({ user, onCancel, onConfirm }) => {

  return (
    <div className="modal-container fixed z-50 flex justify-center items-center top-0 right-0 bottom-0 left-0 bg-black bg-opacity-30">
      <div className="modal bg-white w-96 p-4 rounded-md shadow-lg">
        <div className="mb-4">
          <h2 className="text-xl font-bold">Delete User</h2>
          <p className="text-gray-600">
            Are you sure you want to delete the user:
          </p>
          <p className="font-semibold">
            {`  ${user.email}`}
          </p>
        </div>

        <div className="flex justify-end">
          <button
            className="btn bg-gray-300 hover:bg-gray-400 text-gray-800 mr-2"
            onClick={onCancel}
          >
            Cancel
          </button>
          <button
            className="btn bg-red-500 hover:bg-red-600 text-gray-800"
            onClick={() => onConfirm(user.id)}
          >
            Confirm
          </button>
        </div>
      </div>
    </div>
  );
};

export default DeleteUserModal;