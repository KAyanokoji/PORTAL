import React from 'react'

const ImageLogo = ({
    src,
    alt = "Logo",
    width = "w-32",      // Tailwind width class
    height = "h-auto",   // Tailwind height class
    className = "",
    onClick,
}) => {
    return (
        <div className="flex justify-center items-center">  {/* Flex container for centering */}
            <img
                src={src}
                alt={alt}
                className={`object-contain ${width} ${height} ${className}`}
                onClick={onClick}
            />
        </div>
    )
}

export default ImageLogo