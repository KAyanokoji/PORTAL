'use client'
import React, { useEffect } from 'react';
import { SnackbarProvider, useSnackbar } from 'notistack';
import {
  CheckCircleOutlined,
  InfoCircleOutlined,
  CloseCircleOutlined,
  ExclamationCircleOutlined,
} from '@ant-design/icons';

// Global reference
let _enqueueSnackbar = null;

// HTTP Status to message/icon/variant mapping
const httpStatusMessages = {
  200: { message: 'Success', variant: 'success', icon: <CheckCircleOutlined className="text-green-500" /> },
  201: { message: 'Created', variant: 'success', icon: <CheckCircleOutlined className="text-green-500" /> },
  204: { message: 'No Content', variant: 'info', icon: <InfoCircleOutlined className="text-blue-500" /> },
  400: { message: 'Bad Request', variant: 'error', icon: <CloseCircleOutlined className="text-red-500" /> },
  401: { message: 'Unauthorized', variant: 'error', icon: <CloseCircleOutlined className="text-red-500" /> },
  403: { message: 'Forbidden', variant: 'error', icon: <CloseCircleOutlined className="text-red-500" /> },
  404: { message: 'Not Found', variant: 'error', icon: <CloseCircleOutlined className="text-red-500" /> },
  500: { message: 'Internal Server Error', variant: 'error', icon: <ExclamationCircleOutlined className="text-red-500" /> },
};

export const notify = ({ status, message }) => {
  if (!_enqueueSnackbar) return;

  const fallback = {
    message: 'Unknown Error',
    variant: 'warning',
    icon: <ExclamationCircleOutlined className="text-yellow-500" />,
  };

  const { message: defaultMsg, variant, icon } = httpStatusMessages[status] || fallback;

  _enqueueSnackbar(message || defaultMsg, {
    variant,
    content: (key, msg) => (
      <div
        key={key}
        className="bg-white text-black border border-gray-200 px-4 py-3 rounded-md shadow-md flex items-start gap-2"
      >
        <div className="mt-1">{icon}</div>
        <div>{msg}</div>
      </div>
    ),
  });
};

// Hook initializer
const SnackbarUtilsInitializer = () => {
  const { enqueueSnackbar } = useSnackbar();

  useEffect(() => {
    _enqueueSnackbar = enqueueSnackbar;
  }, [enqueueSnackbar]);

  return null;
};

// Main provider wrapper
const NotificationProvider = ({ children }) => (
  <SnackbarProvider
    maxSnack={3}
    anchorOrigin={{ vertical: 'top', horizontal: 'right' }}
    autoHideDuration={3000}
  >
    <SnackbarUtilsInitializer />
    {children}
  </SnackbarProvider>
);

export default NotificationProvider;
