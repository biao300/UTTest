import React from 'react';

interface DatepickerProps {
    name: string;
    input: string;
    minDate: string;
    touched: boolean;
    error: string;
    handleChange: (event: React.FormEvent<HTMLInputElement>) => void;
}

export default function Datepicker(props: DatepickerProps) {
    const {
        name, 
        input, 
        minDate,
        touched, 
        error,
        handleChange
    } = props;

    let className = "form-control";

    if (touched && error) {
        className += " error-item";
    }

    return (<div>
        <input type="date"
            name={name}
            value={input}
            min={minDate}
            className={className}
            onChange={handleChange}
        />
        {touched && error !== "" && <span className="error-msg">{error}</span>}
    </div>
    );
}