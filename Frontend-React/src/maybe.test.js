import Maybe from './maybe';

test('it should parse a validation error', () => {
    var validationError = {
        "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
        "title": "One or more validation errors occurred.",
        "status": 400,
        "errors": {
            "Description": [
                "The Description field is required."
            ]
        },
        "traceId": "00-c366df286a34322f2a94d2787a741b9e-d14972d10ff6398c-00"
    }

    var error = {
        response: {
            data: validationError
        }
    }

    var maybe = new Maybe(null, error);
    expect(maybe?.error?.title === validationError.title);
    expect(maybe?.error?.body === validationError.errors.Description.join('\n'));
});

test('it should parse a non-validation error', () => {
    var badRequestError = {
        "type": "EntityAlreadyExistsException",
        "title": "Entity Uniqueness exception",
        "status": 400,
        "detail": "Description must be unique"
    }

    var error = {
        response: {
            data: badRequestError
        }
    };
      

    var maybe = new Maybe(null, error);
    expect(maybe?.error?.title == badRequestError.title);
    expect(maybe?.error?.body == badRequestError.detail);

});

