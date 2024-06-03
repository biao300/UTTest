import React, { useState } from 'react';

import TextAreaField from 'components/form/textarea';

export default function EventSearch({ searchClick }: any) {
    const [search, setSearch] = useState("");
    const [searchError, setSearchError] = useState("");
    const [searchTouched, setSearchTouched] = useState(false);

    const handleSearch = () => {
        let searchError = "";
        if (!search && search === "") {
            searchError = "please input search keywords";
        }

        setSearchError(searchError);
        setSearchTouched(true);

        if (searchError == "") {
            searchClick(search);
        }
    }

    return (<>
        <TextAreaField 
            name="search"
            input={search}
            label="Search: "
            maxLength={20}
            rows={1}
            cols={20}
            touched={searchTouched}
            error={searchError}
            handleChange={(e: any) => setSearch(e.currentTarget.value)}
        />

        <p><input type="button" value='Search' onClick={handleSearch} /></p>
    </>)

}