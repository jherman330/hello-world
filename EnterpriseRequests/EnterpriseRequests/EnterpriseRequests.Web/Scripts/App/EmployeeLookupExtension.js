/* Global jQuery extension to turn a textbox into an employee search control.
 * 
 *  For more information on the service itself, see https://employeelookup.fenetwork.com/
 *
 *  To use this extension:
 *  
 *  In your view, lay down your inputs for employee information:
 *
 *      @Html.TextBoxFor(x => x.EmployeeName)
 *      @Html.TextBoxFor(x => x.SAPId)
 *  
 *  In your view's scripts section, define your event handlers:
 *   
 *      var setData = function(dataItem) {
 *          $("#EmployeeName").val(dataItem.DisplayName);
 *          $("#SAPId").val(dataItem.SAPId);
 *      };
 *      var clearData = function() {
 *          $("#SAPId").val("");
 *          $("#EmployeeName").val("");
 *      };
 *      var onBlur = function() {
 *          if(!$("#SAPId").val()) {
 *              $("#EmployeeName").val("");
 *          }
 *      };
 *
 *  Then call the extension, passing in your event handlers:
 *  
 *      $("#EmployeeName").EmployeeLookup(setData, clearData, onBlur);
 *
 *  By default, this extension calls the simple search of employees.
 *  This can be overridden by passing in a boolean true as a fourth parameter:
 *      
 *      $("#EmployeeName").EmployeeLookup(setData, clearData, onBlur, true);
 *
 *  This will return the full employee model. 
 *
 *  Note: Editing this file may lead to unexpected behavior.
 */

(function ($) {
    $.fn.EmployeeLookup = function (set, clear, blur, useFullEmployeeModel) {
        var url = "";
        try {
            url = defaults.empBaseAPI;
        } catch (e) {
            toastr.error("Employee Lookup Service not found:<br />" + e.message);
            console.error("Error: Employee Lookup Service not found: " + e.message);
            return this;
        }

        if (useFullEmployeeModel === true) {
            url += "employees/v1/{searchText}/search";
        } else {
            url += "employees/v1/{searchText}/search/simple";
        }

        var object = this;
        this.kendoAutoComplete({
            dataSource: {
                transport: {
                    read: {
                        url: function () {
                            //debugger;
                            return url.replace("{searchText}", object.val().trim());
                        },
                        dataType: "json"
                    },
                    parameterMap: function (data, type) {
                        //debugger;
                        if (data.filter) {
                            data.filter = null;
                        }
                    }
                },
                serverFiltering: true,
                requestEnd: function (e) {
                    //debugger;
                    if (e.response && e.response.length === 0) {
                        e.response.push({ DisplayNameWithId: "0 results found", PreventDefault: true });
                    }
                }
            },
            filtering: function (e) {
                //debugger;
                var filter = e.filter;
                if (!filter.value) {
                    clear();
                    e.preventDefault();
                }
            },
            dataTextField: "DisplayNameWithId",
            minLength: 3,
            highlightFirst: true,
            select: function (e) {
                //debugger;
                e.preventDefault();
                var dataItem = this.dataItem(e.item.index());
                if (dataItem) {
                    if (dataItem.PreventDefault) {
                        clear();
                    }
                    else {
                        set(dataItem);
                    }
                }
            },
            change: function (e) {
                //debugger;
                if (!this.value()) {
                    clear();
                }
            }
        });

        this.blur(blur);
        return this;
    };
}(jQuery));