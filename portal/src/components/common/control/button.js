'use client'
import React from 'react';

const Button = ({
  children,
  onClick,
  type = 'button',
  variant = 'primary', // primary, secondary, success, danger, warning, info
  style = 'filled', // filled, outlined
  shape = 'rounded', // rounded, rectangle
  size = 'medium', // small, medium, large
  width = 'auto', // auto, block, full
  disabled = false,
  className = '',
  icon,
  iconPosition = 'left', // left, right
  label = 'Button',
}) => {
  // Color variants
  const colorVariants = {
    primary: {
      filled: 'bg-blue-600 hover:bg-blue-700 text-white',
      outlined: 'border border-blue-600 text-blue-600 hover:bg-blue-50 bg-white',
    },
    secondary: {
      filled: 'bg-gray-600 hover:bg-gray-700 text-white',
      outlined: 'border border-gray-600 text-gray-600 hover:bg-gray-50 bg-white',
    },
    success: {
      filled: 'bg-green-600 hover:bg-green-700 text-white',
      outlined: 'border border-green-600 text-green-600 hover:bg-green-50 bg-white',
    },
    danger: {
      filled: 'bg-red-600 hover:bg-red-700 text-white',
      outlined: 'border border-red-600 text-red-600 hover:bg-red-50 bg-white',
    },
    warning: {
      filled: 'bg-yellow-500 hover:bg-yellow-600 text-white',
      outlined: 'border border-yellow-500 text-yellow-600 hover:bg-yellow-50 bg-white',
    },
    info: {
      filled: 'bg-cyan-500 hover:bg-cyan-600 text-white',
      outlined: 'border border-cyan-500 text-cyan-600 hover:bg-cyan-50 bg-white',
    },
  };

  // Size variants
  const sizeVariants = {
    small: `
      py-1 px-2 text-xs      /* Mobile (320px+) */
      sm:py-1.5 sm:px-3      /* Tablet (480px+) */
      md:py-1 md:px-3 md:text-sm  /* Desktop (768px+) */
    `,
    medium: `
      py-1.5 px-3 text-sm    /* Mobile */
      sm:py-2 sm:px-4        /* Tablet */
      md:py-2 md:px-4 md:text-base  /* Desktop */
    `,
    large: `
      py-2 px-4 text-base    /* Mobile */
      sm:py-2.5 sm:px-5      /* Tablet */
      md:py-3 md:px-6 md:text-lg  /* Desktop */
    `
  };

  // Shape variants
  const shapeVariants = {
    rounded: 'rounded-lg sm:rounded-xl md:rounded-full',
    rectangle: 'rounded-sm sm:rounded-md'
  };

  // Width variants
  const widthVariants = {
    auto: 'w-auto',
    block: 'w-full sm:w-auto sm:min-w-[120px] md:min-w-[140px]',
    full: 'w-full'
  };

    // Responsive icon sizing
    const iconSize = {
        small: 'h-3 w-3 sm:h-3.5 sm:w-3.5',
        medium: 'h-4 w-4 sm:h-5 sm:w-5',
        large: 'h-5 w-5 sm:h-6 sm:w-6'
      };

  // Disabled state
  const disabledClasses =disabled ? 'opacity-60 cursor-not-allowed' : 'cursor-pointer hover:shadow-sm active:scale-[0.98]'

  // Build button classes
  const buttonClasses = [
    'flex items-center justify-center font-medium transition-colors duration-200',
    colorVariants[variant]?.[style] || colorVariants.primary.filled,
    sizeVariants[size],
    shapeVariants[shape],
    widthVariants[width],
    disabledClasses,
    className,
  ].join(' ');

  return (
    <button
      type={type}
      onClick={onClick}
      disabled={disabled}
      className={buttonClasses}
    >
       {icon && iconPosition === 'left' && (
        <span className={`mr-1 sm:mr-2 ${iconSize[size]}`}>
          {React.cloneElement(icon, { className: 'w-full h-full' })}
        </span>
      )}
      <span className="whitespace-nowrap">
        {label}
      </span>
      {icon && iconPosition === 'right' && (
        <span className={`ml-1 sm:ml-2 ${iconSize[size]}`}>
          {React.cloneElement(icon, { className: 'w-full h-full' })}
        </span>
      )}
    </button>
  );
};

export default Button;