"use strict";
var AjaxHelper = (function () {

    var _pendingRequests = 0;

    var _changeBlockUIState = function () {
        if (_pendingRequests) {
            $.blockUI({
                message: '<h1><i class="fas fa-spinner fa-spin"></i></h1>',
                css: {
                    border: 'none',
                    padding: '30px'
                }
            });
        } else {
            $.unblockUI();
        }
    };

    var _changeBlockUIStateDebounced = _.debounce(_changeBlockUIState, 500);

    var _performMethod = function (urlApi, methodType, data, success, error, options) {
        options = options || {};
        options.disableLoadingIndicator = options.disableLoadingIndicator || false;

        var disableLoadingIndicator = options.disableLoadingIndicator;
        options.disableLoadingIndicator = true;

        return $.ajax($.extend({
            url: urlApi,
            type: methodType,
            data: JSON.stringify(data),
            dataType: 'json',
            contentType: 'application/json; charset=UTF-8',
            success: function (data, textStatus, xhr) {
                if (typeof success === "function") {
                    success(data, textStatus, xhr);
                } else {
                    console.log(success);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                if (typeof error === "function") {
                    error(xhr, textStatus, errorThrown);
                } else {
                    console.log(error);
                }
            },
            beforeSend: function () {
                if (!disableLoadingIndicator) {
                    _pendingRequests++;
                    _changeBlockUIState();
                }
            },
            complete: function () {
                if (!disableLoadingIndicator) {
                    _pendingRequests--;
                    _changeBlockUIStateDebounced();
                }
            }
        }, options));
    };

    var _getParamsString = function (model) {
        var getNestedObjectsParamsString = function (name, nestedObject) {
            if (Array.prototype.isPrototypeOf(nestedObject)) {
                return getNestedArrayParamsString(name, nestedObject);
            }

            return Object.keys(nestedObject)
                .map(function (key) {
                    var value = nestedObject[key]
                        , currentKeyName = name + '.' + key;

                    if (value !== null && typeof value === 'object')
                        return getNestedObjectsParamsString(currentKeyName, value);

                    return currentKeyName + '=' + encodeURIComponent(value);
                })
                .join('&');
        };

        var getNestedArrayParamsString = function (name, array) {
            return array
                .map(function (value, index) {
                    var currentKeyName = name + '[' + index + ']';

                    if (value !== null && typeof value === 'object')
                        return getNestedObjectsParamsString(currentKeyName, value);

                    return currentKeyName + '=' + encodeURIComponent(value);
                })
                .join('&');
        };

        if (model !== null && typeof model === 'object') {
            var paramsArray = Object.keys(model)
                .map(function (key) {
                    var value = model[key];
                    if (value !== null && typeof value === 'object')
                        return getNestedObjectsParamsString(key, value);

                    return key + '=' + encodeURIComponent(value);
                });

            return paramsArray.length ? '?' + paramsArray.join('&') : '';
        }
        return '';
    };

    return {
        get: function (urlApi, params, success, error, options) {
            return _performMethod(urlApi + _getParamsString(params), "GET", null, success, error, options);
        },
        post: function (urlApi, data, success, error, options) {
            return _performMethod(urlApi, "POST", data, success, error, options);
        },
        put: function (urlApi, data, success, error, options) {
            return _performMethod(urlApi, "PUT", data, success, error, options);
        },
        delete: function (urlApi, data, success, error, options) {
            return _performMethod(urlApi, "DELETE", data, success, error, options);
        },
        patch: function (urlApi, data, success, error, options) {
            return _performMethod(urlApi, "PATCH", data, success, error, options);
        },
        redirect: function (url, data) {
            var destination = url;
            if (data !== undefined) {
                destination += _getParamsString(data);
            }
            window.location.href = destination;
        },
        redirectBlank: function (url, data) {
            var destination = url;
            if (data !== undefined) {
                destination += _getParamsString(data);
            }
            window.open(destination, '_blank');
        },
        getParamsString: _getParamsString
    };
})();

