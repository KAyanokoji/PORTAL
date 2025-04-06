import { call, put, takeLatest } from "redux-saga/effects";
import { httpAuthenticateUser } from "../http/auth";

const { loginUser, loginFailure, loginSuccess } = require("./authSlice");


function* handleLogin(action){
  console.log("ACTION DATA", action)
  try {
    const result = yield call(httpAuthenticateUser, action.payload);
    if (result.success) {
      yield put(loginSuccess(result.data));
    } else {
      yield put(loginFailure(result.message));
    }
  } catch (error) {
    yield put(loginFailure(error.message));
  }
}


// Watcher saga
function* authSaga() {
    // Listen for the loginUser action and call the handleLogin saga when dispatched
    yield takeLatest(loginUser.type, handleLogin)
  }

export default authSaga;
