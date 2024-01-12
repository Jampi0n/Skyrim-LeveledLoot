using System;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins;

namespace LeveledLoot {
    class RecordException : Exception {

        string recordDescription;

        public RecordException(IFormLinkGetter record, string message) : base("[" + record.FormKey + "~?]: " + message) { }

        public RecordException(IMajorRecordGetter record, string message) : base("[" + record.FormKey + "~" + record.EditorID + "]: " + message) { }
    }
}
