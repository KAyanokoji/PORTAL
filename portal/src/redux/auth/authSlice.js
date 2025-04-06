import { createSlice,nanoid } from '@reduxjs/toolkit';

const initialState = {
    email: '',
    password: '',
    isLoading: false,
    isLoggedIn: false,
    error: null,
    user: null
  };


  const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
      // For handling input changes
      setEmail: (state, action) => {
        state.email = action.payload;
      },
      setPassword: (state, action) => {
        state.password = action.payload;
      },
      // For login process
      loginUser: (state) => {
        state.isLoading = true;
        state.error = null;
      },
      loginSuccess: (state, action) => {
        state.isLoading = false;
        state.isLoggedIn = true;
        state.user = action.payload;
        state.error = null;
      },
      loginFailure: (state, action) => {
        console.log("FAILURE", action)
        state.isLoading = false;
        state.error = action.payload;
      },
      // For logout
      logout: (state) => {
        state.isLoggedIn = false;
        state.user = null;
        state.email = '';
        state.password = '';
      }
    }
  });


  export const { 
    setEmail, 
    setPassword, 
    loginUser, 
    loginSuccess, 
    loginFailure, 
    logout 
  } = authSlice.actions;



  export default authSlice.reducer;
