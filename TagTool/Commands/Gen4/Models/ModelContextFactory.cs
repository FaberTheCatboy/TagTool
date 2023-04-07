﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagTool.Cache;
using TagTool.Tags.Definitions.Gen4;

namespace TagTool.Commands.Gen4.Models
{
    static class ModelContextFactory
    {
        public static CommandContext Create(CommandContext parent, GameCache cache, CachedTag tag, Model hlmt)
        {
            var groupName = tag.Group.ToString();

            var context = new CommandContext(parent,
                string.Format("{0:X8}.{1}", tag.Index, groupName));

            Populate(context, cache, tag, hlmt);

            return context;
        }

        public static void Populate(CommandContext commandContext, GameCache cache, CachedTag tag, Model hlmt)
        {
            commandContext.AddCommand(new ExportJMSCommand(cache, hlmt));
        }
    }
}
