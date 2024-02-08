import { Suspense, useEffect, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Toaster } from 'react-hot-toast';
import ECommerce from './pages/Dashboard/ECommerce';
import SignIn from './pages/Authentication/SignIn';
//import SignUp from './pages/Authentication/SignUp';
import Loader from './common/Loader';
import routes from './routes';
import Users from './pages/users';
import AddUserPage from './pages/users/create';
import Apartments from './pages/apartments';
import AddApartment from './pages/apartments/create';
import { useTypedSelector } from './hooks/useTypedSelector.ts';
import DefaultLayout from './layout/DefaultLayout'
import EditUserPage from './pages/users/update';
import UpdateApartment from './pages/apartments/update';
import EmailConfirmation from './pages/users/auth/confirmEmail.tsx';

function App() {
  const [loading, setLoading] = useState<boolean>(true);
  const { isAuth } = useTypedSelector((store) => store.UserReducer);
console.log("auth", isAuth)
  useEffect(() => {
    setTimeout(() => setLoading(false), 1000);
  }, []);

  return loading ? (
    <Loader />
  ) : (
    <>
      <Toaster
        position="top-right"
        reverseOrder={false}
        containerClassName="overflow-auto"
      />
      <Routes>

        {isAuth && (
          <>
            ((
            <Route  element={<DefaultLayout />}>
              <Route path="/dashboard" element={<ECommerce />} />
              <Route path="/dashboard/users" element={<Users />} />
              <Route path="/dashboard/users/edit/:userId" element={<EditUserPage />} />
              <Route path="/dashboard/adduser" element={<AddUserPage />} />
              <Route path="/dashboard/apartments" element={<Apartments />} />
              <Route path="/dashboard/addapartment" element={<AddApartment />} />
              <Route path="/dashboard/apartment/edit/:apartmentId" element={<UpdateApartment />} />

              {routes.map((routes, index) => {
                const { path, component: Component } = routes;
                return (
                  <Route
                    key={index}
                    path={path}
                    element={
                      <Suspense fallback={<Loader />}>
                        <Component />
                      </Suspense>
                    }
                  />
                );
              })}
            </Route>
            ))
          </>
        )}

        <Route path="*" element={<SignIn />} />
        <Route path="/confirmemail/:userId/:token" element={<EmailConfirmation/>} />
        {/*<Route path="/dashboard" element={<SignIn />} />*/}
        {/*<Route path="/signup" element={<SignUp />} />*/}
      </Routes>
    </>
  );
}

export default App;
