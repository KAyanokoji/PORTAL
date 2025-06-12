'use client';
import { useState, useMemo, useCallback } from 'react';

const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Moved outside component to avoid recreation

const EmailInput = ({
  id = 'email',
  name = 'email',
  label = 'Email',
  placeholder = 'your@email.com',
  required = false,
  value = '',
  onChange,
  onBlur,
  errorMessage = 'Please enter a valid email address',
  successMessage = 'Email address is valid',
  className = 'mb-4',
  inputClassName = 'w-full px-3 py-2 bg-white border rounded-md shadow-sm focus:outline-none focus:ring-blue-500',
  labelClassName = 'text-sm font-medium text-gray-700',
  errorClassName = 'mt-1 text-sm text-red-600',
  successClassName = 'mt-1 text-sm text-green-600',
  layout = 'block',
  labelPosition = 'top',
  gap = '2'
}) => {
  const [email, setEmail] = useState(value);
  const [touched, setTouched] = useState(false);
  
  // Memoized validation
  const isValid = useMemo(() => emailRegex.test(email), [email]);

  // Event handlers
  const handleChange = useCallback((e) => {
    const newEmail = e.target.value;
    setEmail(newEmail);
    onChange?.(newEmail, emailRegex.test(newEmail));
  }, [onChange]);

  const handleBlur = useCallback(() => {
    setTouched(true);
    onBlur?.(email, isValid);
  }, [email, isValid, onBlur]);

  // Derived states
  const showError = touched && !isValid && email.length > 0;
  const showSuccess = touched && isValid;

  // Class generators
  const containerClasses = useMemo(() => {
    const layoutClasses = layout === 'inline' ? 'flex items-center' : 'flex flex-col';
    return `${className} ${layoutClasses} gap-${gap}`.trim();
  }, [layout, gap, className]);

  const labelClasses = useMemo(() => {
    const positionClasses = 
      labelPosition === 'left' ? 'mr-2' : 
      labelPosition === 'right' ? 'ml-2 order-1' : '';
    return `${labelClassName} ${positionClasses}`.trim();
  }, [labelPosition, labelClassName]);

  const inputClasses = useMemo(() => {
    const stateClasses = 
      showError ? 'border-red-500 focus:ring-red-500' :
      showSuccess ? 'border-green-500 focus:ring-green-500' :
      'border-gray-300';
    return `${inputClassName} ${stateClasses}`.trim();
  }, [showError, showSuccess, inputClassName]);

  return (
    <div className={containerClasses}>
      {labelPosition !== 'hidden' && (
        <label htmlFor={id} className={labelClasses}>
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
          className={inputClasses}
          required={required}
        />
      </div>

      {showError && <p className={errorClassName}>{errorMessage}</p>}
      {/* {showSuccess && <p className={successClassName}>{successMessage}</p>} */}
    </div>
  );
};

export default EmailInput;