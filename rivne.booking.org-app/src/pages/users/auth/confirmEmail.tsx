// Your React component

import { useDispatch } from 'react-redux';
import { useParams } from 'react-router-dom'; // If you're using React Router


import { useEffect } from 'react';
import { useActions } from '../../../hooks/useActions.ts';

const ConfirmationComponent = () => {
  const dispatch = useDispatch();
  const { ConfirmEmail } = useActions();
  const { userId, token } = useParams(); // Extract userId and token from the URL parameters

  useEffect(() => {
    // Dispatch the confirmEmail action
    // @ts-ignore
    dispatch(ConfirmEmail(userId, token));
  }, [dispatch, userId, token]);

  // ... rest of your component logic
};

export default ConfirmationComponent;
