namespace PriceAlert.API.Errors;

public class MissingRequiredPropertyError(string property) : Error($"""missing required property: '{property}'.""");
