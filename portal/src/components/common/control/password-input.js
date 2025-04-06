'use client';
import { useState } from 'react';
import { Eye, EyeOff } from 'lucide-react';

const PasswordInput = ({
  id = 'password',
  name = 'password',
  label = '',
  placeholder = 'Enter your password',
  required = false,
  value = '',
  onChange,
  onBlur,
  errorMessage = 'Password must contain at least: 8 characters, one number, one special character, one uppercase and one lowercase letter',
  successMessage = 'Password is valid',
  className = '',
  inputClassName = '',
  labelClassName = '',
  errorClassName = '',
  successClassName = '',
  layout = 'block',
  labelPosition = 'top',
  gap = 2,
  minLength = 8
}) => {
  const [password, setPassword] = useState(value);
  const [touched, setTouched] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [isValid, setIsValid] = useState(false);
  const [validationErrors, setValidationErrors] = useState([]);

  const validatePassword = (password) => {
    const errors = [];
    
    if (password.length < minLength) {
      errors.push(`At least ${minLength} characters`);
    }
    if (!/[0-9]/.test(password)) {
      errors.push('One number');
    }
    if (!/[a-z]/.test(password)) {
      errors.push('One lowercase letter');
    }
    if (!/[A-Z]/.test(password)) {
      errors.push('One uppercase letter');
    }
    if (!/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password)) {
      errors.push('One special character');
    }

    setValidationErrors(errors);
    return errors.length === 0;
  };

  const handleChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    const valid = validatePassword(newPassword);
    setIsValid(valid);
    if (onChange) onChange(newPassword, valid);
  };

  const handleBlur = () => {
    setTouched(true);
    if (onBlur) onBlur(password, isValid);
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const showError = touched && !isValid && password.length > 0;
  const showSuccess = touched && isValid;

  // Layout classes (same as before)
  const getContainerClasses = () => {
    let classes = 'mb-4';
    if (layout === 'inline') classes += ' flex items-center';
    else if (layout === 'block') classes += ' flex flex-col';
    if (gap) classes += ` gap-${gap}`;
    return `${classes} ${className}`;
  };

  const getLabelClasses = () => {
    let classes = 'text-sm font-medium text-gray-700';
    if (labelPosition === 'left') classes += ' mr-2';
    else if (labelPosition === 'right') classes += ' ml-2 order-1';
    return `${classes} ${labelClassName}`;
  };

  return (
    <div className={getContainerClasses()}>
      {labelPosition !== 'hidden' && (
        <label htmlFor={id} className={getLabelClasses()}>
          {label}
          {required && <span className="text-red-500">*</span>}
        </label>
      )}

      <div className="flex-1 relative">
        <input
          type={showPassword ? 'text' : 'password'}
          id={id}
          name={name}
          value={password}
          onChange={handleChange}
          onBlur={handleBlur}
          placeholder={placeholder}
          className={`w-full px-3 py-2 pr-10 bg-white border rounded-md shadow-sm focus:outline-none focus:ring-blue-500 ${
            showError
              ? 'border-red-500 focus:ring-red-500'
              : showSuccess
              ? 'border-green-500 focus:ring-green-500'
              : 'border-gray-300'
          } ${inputClassName}`}
          required={required}
          minLength={minLength}
        />
        <button
          type="button"
          onClick={togglePasswordVisibility}
          className="absolute inset-y-0 right-0 flex items-center pr-3 text-gray-500 hover:text-gray-700 focus:outline-none"
          aria-label={showPassword ? 'Hide password' : 'Show password'}
        >
          {showPassword ? <EyeOff className="h-5 w-5" /> : <Eye className="h-5 w-5" />}
        </button>
      </div>

      {showError && (
        <div className="mt-1">
          <p className={`text-sm text-red-600 ${errorClassName}`}>
            {errorMessage}
          </p>
          <ul className="text-xs text-red-500 mt-1 list-disc list-inside">
            {validationErrors.map((error, index) => (
              <li key={index}>{error}</li>
            ))}
          </ul>
        </div>
      )}

      {showSuccess && (
        <p className={`mt-1 text-sm text-green-600 ${successClassName}`}>
          {successMessage}
        </p>
      )}
    </div>
  );
};

export default PasswordInput;