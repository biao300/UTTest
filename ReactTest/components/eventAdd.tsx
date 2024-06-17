import React, { useState } from 'react';

import TextAreaField from 'components/form/textarea';
import Datepicker from './form/datepicker';

export default function EventAdd({ submitClick }: any) {
    const [title, setTitle] = useState("");
    const [titleError, setTitleError] = useState("");
    const [titleTouched, setTitleTouched] = useState(false);

    const [description, setDescription] = useState("");
    const [descriptionError, setDescriptionError] = useState("");
    const [descriptionTouched, setDescriptionTouched] = useState(false);

    const [date, setDate] = useState("");
    const [dateError, setDateError] = useState("");
    const [dateTouched, setDateTouched] = useState(false);

    const [formMessage, setFormMessage] = useState("");

    const checkValidation = ({title, description, date}: any) => {
        let titleMsg = "";
        let descriptionMsg = "";
        let dateMsg = "";

        if (!title && title === "") {
            titleMsg = "Please input title";
        }

        if (!description && description === "") {
            descriptionMsg = "Please input description";
        }

        if (!date && date === "") {
            dateMsg = "Please input event date";
        }

        setTitleTouched(true);
        setDescriptionTouched(true);
        setDateTouched(true);
        
        setTitleError(titleMsg);
        setDescriptionError(descriptionMsg);
        setDateError(dateMsg);

        setFormMessage("");

        return titleMsg === "" && descriptionMsg === "" && dateMsg === "";
    }

    const handleSubmit = (event: any) => {
        event.preventDefault();

        const addItem = {
            title: event.target.title.value,
            description: event.target.description.value,
            date: event.target.date.value
        }

        const valid = checkValidation(addItem);

        if (valid) {
            submitClick(event);

            setTitle("");
            setDescription("");
            setDate("");
        }
    }

    return (<form onSubmit={handleSubmit} className='form'>
        <h2>Add new event</h2>
        <TextAreaField 
            name="title"
            input={title}
            label="Title: "
            maxLength={20}
            rows={1}
            cols={20}
            touched={titleTouched}
            error={titleError}
            handleChange={(e: any) => setTitle(e.currentTarget.value)}
        />
        <TextAreaField 
            name="description"
            input={description}
            label="Description: "
            maxLength={200}
            rows={10}
            cols={20}
            touched={descriptionTouched}
            error={descriptionError}
            handleChange={(e: any) => setDescription(e.currentTarget.value)}
        />

        <Datepicker
            name="date"
            input={date}
            minDate={new Date().toISOString().split('T')[0]}
            touched={dateTouched}
            error={dateError}
            handleChange={(e: any) => setDate(e.currentTarget.value)}
        />

        <p><input type="submit" value='Add Event' /></p>
        <p>{formMessage}</p>
    </form>)
}