/* eslint-disable */

function dateSerializer(dateString) {
    const date = new Date(dateString);
    return `${date.getDate()}-${date.getMonth()}-${date.getFullYear()} ${date.getHours()}:${date.getMinutes().toString().padStart(2, '0')}`;
}

export {dateSerializer}