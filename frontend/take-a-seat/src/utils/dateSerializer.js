/* eslint-disable */

function dateSerializer(dateString) {
    const date = new Date(dateString);    
    return `${date.getDate().toString().padStart(2, '0')}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getFullYear()} ${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}`;
}

function dateUTCSerializer(dateString) {
    const date = new Date(dateString);    
    return `${date.getFullYear()}-${(date.getMonth() + 1).toString().padStart(2, '0')}-${date.getDate().toString().padStart(2, '0')}T${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}:00.000Z`;
}

export {dateSerializer, dateUTCSerializer}