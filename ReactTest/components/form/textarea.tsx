import React from 'react';

interface TextAreaFieldProps {
    name: string;
    input: string;
    label: string;
    rows: number;
    cols: number;
    maxLength: number;
    touched: boolean;
    error: string;
    handleChange: (event: React.FormEvent<HTMLTextAreaElement>) => void;
}

export default function TextAreaField(props: TextAreaFieldProps) {

    const {
        name, 
        input, 
        label, 
        rows, 
        cols, 
        maxLength, 
        touched, 
        error,
        handleChange
    } = props;

    let className = "form-control";

    if (touched && error) {
        className += " error-item";
    }

    return (<>
        <textarea name={name}
            value={input}
            placeholder={label}
            className={className}
            style={{ resize: "none" }}
            rows={rows}
            cols={cols}
            maxLength={maxLength}
            onChange={handleChange}
        />
        {touched && error !== "" && <span className="error-msg">{error}</span>}
        </>
    );
}