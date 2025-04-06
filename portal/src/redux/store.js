import { configureStore } from "@reduxjs/toolkit";
import authReducer from '@/redux/auth/authSlice';
import createSagaMiddleware from "redux-saga";
import rootSaga from "./rootSaga";


// Create the saga middleware
const sagaMiddleware = createSagaMiddleware();

// Configure the Redux store with the middleware
const store = configureStore({
  reducer: {
    auth:authReducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ thunk: false }).concat(sagaMiddleware),
});

// Run the root saga
sagaMiddleware.run(rootSaga);
export default store;