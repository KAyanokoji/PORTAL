import { call, put, takeLatest } from "redux-saga/effects";
import { httpAuthenticateUser } from "../http/auth";
import { notify } from "@/components/common/control/SetupSnackbar.";
const { loginUser, loginFailure, loginSuccess } = require("./authSlice");


function* handleLogin(action){
  try {
    const result = yield call(httpAuthenticateUser, action.payload);
    if (result.success) {
      notify(result)
      // displayNotification(result);
      yield put(loginSuccess(result.data));
    } else {
      notify(result)
      yield put(loginFailure(result.message));
    }
  } catch (error) {
    notify(error)
    yield put(loginFailure(error.message));
  }
}


// Watcher saga
function* authSaga() {
    // Listen for the loginUser action and call the handleLogin saga when dispatched
    yield takeLatest(loginUser.type, handleLogin)
  }

export default authSaga;
