'use client';
import { useState } from 'react';

const EmailInput = ({
  id = 'email',
  name = 'email',
  label = '',
  placeholder = 'your@email.com',
  required = false,
  value = '',
  onChange,
  onBlur,
  errorMessage = 'Please enter a valid email address',
  successMessage = 'Email address is valid',
  className = '',
  inputClassName = '',
  labelClassName = '',
  errorClassName = '',
  successClassName = '',
  layout = 'block',
  labelPosition = 'top',
  gap = 2
}) => {
  const [email, setEmail] = useState(value);
  const [touched, setTouched] = useState(false);
  const [isValid, setIsValid] = useState(false);

  const validateEmail = (email) => {
    const re = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(email);
  };

  const handleChange = (e) => {
    console.log("EMail", e.target.value)
    const newEmail = e.target.value;
    setEmail(newEmail);
    const valid = validateEmail(newEmail);
    setIsValid(valid);
    if (onChange) onChange(newEmail, valid);

  };
  console.log("vALIDATE", email)

  const handleBlur = () => {
    setTouched(true);
    if (onBlur) onBlur(email, isValid);
  };

  const showError = touched && !isValid && email.length > 0;
  const showSuccess = touched && isValid;

  // Simplified layout classes
  const getContainerClasses = () => {
    let classes = 'mb-4';
    
    if (layout === 'inline') {
      classes += ' flex items-center';
    } else if (layout === 'block') {
      classes += ' flex flex-col';
    }
    
    if (gap) {
      classes += ` gap-${gap}`;
    }
    
    return `${classes} ${className}`;
  };

  const getLabelClasses = () => {
    let classes = 'text-sm font-medium text-gray-700';
    
    if (labelPosition === 'left') {
      classes += ' mr-2';
    } else if (labelPosition === 'right') {
      classes += ' ml-2 order-1';
    }
    
    return `${classes} ${labelClassName}`;
  };

  return (
    <div className={getContainerClasses()}>
      {labelPosition !== 'hidden' && (
        <label
          htmlFor={id}
          className={getLabelClasses()}
        >
          {label}
          {required && <span className="text-red-500">*</span>}
        </label>
      )}

      <div className="flex-1">
        <input
          type="email"
          id={id}
          name={name}
          value={email}
          onChange={handleChange}
          onBlur={handleBlur}
          placeholder={placeholder}
          className={`w-full px-3 py-2 bg-white border rounded-md shadow-sm focus:outline-none focus:ring-blue-500 ${
            showError
              ? 'border-red-500 focus:ring-red-500'
              : showSuccess
              ? 'border-green-500 focus:ring-green-500'
              : 'border-gray-300'
          } ${inputClassName}`}
          required={required}
        />
      </div>

      {showError && (
        <p className={`mt-1 text-sm text-red-600 ${errorClassName}`}>
          {errorMessage}
        </p>
      )}
      {showSuccess && (
        <p className={`mt-1 text-sm text-green-600 ${successClassName}`}>
          {successMessage}
        </p>
      )}
    </div>
  );
};

export default EmailInput;