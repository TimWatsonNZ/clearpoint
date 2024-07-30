
function Maybe(value, error) {
    this.value = value;
    if (error) {
        this.error = parseError(error);
    }
}

function parseError(error) {
    let title = "";
    let message = "";

    const errorData = error.response.data;
    title = errorData.title ?? "Response missing title";

    if (errorData.detail) {
        message = errorData.detail;
    } else {
        message = errorData.errors.Description.join('\n');
    }

    return {
        title,
        message
    }
}

export default Maybe;
