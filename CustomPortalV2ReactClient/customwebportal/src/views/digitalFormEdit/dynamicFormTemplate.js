import React, { useState, useEffect } from 'react';



function dynamicFormTemplate() {
    const [htmlContent, setHtmlContent] = useState('');

    const loadHtmlFromFile = (event) => {
        const file = event.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (e) => {
                setHtmlContent(e.target.result);
            };
            reader.readAsText(file);
        }
    };

    return (
        <>
            <div
                dangerouslySetInnerHTML={{ __html: htmlContent }}
            />
        </>
    );
}

export default dynamicFormTemplate; 