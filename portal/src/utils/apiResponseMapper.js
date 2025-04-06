// utils/apiResponseMapper.js

export const mapApiSuccessResponse = (response) => {
    return {
      success: true,
      status: response.status,
      data: response.data,
    };
  };
  
  export const mapApiErrorResponse = (error) => {
    if (error.response) {
      return {
        success: false,
        status: error.response.status,
        message: error.response.data?.message || 'Something went wrong',
        data: error.response.data,
      };
    } else if (error.request) {
      return {
        success: false,
        status: 0,
        message: 'No response received from server',
      };
    } else {
      return {
        success: false,
        status: 0,
        message: error.message,
      };
    }
  };
  