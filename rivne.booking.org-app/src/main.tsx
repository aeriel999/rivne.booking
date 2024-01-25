import ReactDOM from 'react-dom/client';

import { BrowserRouter as Router } from 'react-router-dom';

import App from './App';
import './index.css';
import './satoshi.css';
import { getAccessToken } from './services/userServices';
import { AuthUther } from './store/actions/userActionCreator';
import { store } from './store';
import { Provider } from "react-redux";
//import { ToastContainer } from 'react-toastify';

const token = getAccessToken();

if (token) {
  AuthUther(token, "Data Loadet", store.dispatch);
}

ReactDOM.createRoot(document.getElementById('root') as HTMLElement).render(
  <Provider store={store}>
    <Router>
      {/*<ToastContainer autoClose={3000} />*/}
      <App />
    </Router>
  </Provider>
);
